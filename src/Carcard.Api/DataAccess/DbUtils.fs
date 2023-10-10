namespace Carcard.Api.DataAccess

open System
open System.Data.Common
open System.Data.SQLite
open System.Threading.Tasks
open Carcard.Database.Entities
open Carcard.Database.Enums

type EntityData = {
    Id: Guid
    EntityStatus: eEntityStatus
    CreatedAt: DateTime
    ModifiedAt: DateTime
} with
    static member Empty = {
        Id = Guid.Empty
        EntityStatus = eEntityStatus.Active
        CreatedAt = DateTime.MinValue
        ModifiedAt = DateTime.MinValue
    }

module EntityData =
    let ofBaseEntity (x: BaseEntity) =
        {
            Id = x.Id
            EntityStatus = x.EntityStatus
            CreatedAt = x.CreatedAt
            ModifiedAt = x.ModifiedAt
        }

    let populateBaseEntity (e: BaseEntity) (ed: EntityData) =
        e.Id <- ed.Id
        e.EntityStatus <- ed.EntityStatus
        e.CreatedAt <- ed.CreatedAt
        e.ModifiedAt <- ed.ModifiedAt


    let createNew () =
        let now = DateTime.UtcNow
        {
            Id = Guid.NewGuid()
            EntityStatus = eEntityStatus.Active
            CreatedAt = now
            ModifiedAt = now
        }


type DbRecord<'a, 'b> = {
    Model: 'a
    EntityData: EntityData
    EntityRelations: 'b
}

module DbUtils =
    let read2<'T> (reader: DbDataReader) (x: 'T) =
        nameof x |> reader.GetOrdinal

    let read (property: string) (readOrdinal: int -> 'a) (reader: DbDataReader) =
        property
        |> reader.GetOrdinal
        |> readOrdinal


    let mapDbRecord (rdr: DbDataReader) (modelMapper: unit -> 'a) (relationsMapper: unit -> 'b) =
        let entityData = {
            Id = read (nameof Unchecked.defaultof<EntityData>.Id) rdr.GetGuid rdr
        }

        {
            Model = modelMapper ()
            EntityData = entityData
            EntityRelations = relationsMapper ()
        }


    let execute (formatter: SQLiteCommand -> unit, operation: SQLiteCommand -> Task<'a>) = task {
        use conn = new SQLiteConnection(Startup.getDbConnectionString ())
        use cmd = new SQLiteCommand(conn)
        do formatter cmd
        
        do! conn.OpenAsync()
        let! result = operation cmd
        do! conn.CloseAsync()

        return result
    }

