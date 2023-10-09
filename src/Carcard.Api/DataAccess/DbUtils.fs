namespace Carcard.Api.DataAccess

open System
open System.Data.Common
open System.Data.SQLite
open System.Threading.Tasks

type EntityData = {
    Id: Guid
} with
    static member Empty = {
        Id = Guid.Empty
    }
    static member New = {
        Id = Guid.NewGuid()
    }

module EntityData =
    let create (createdAt: DateTime) (modifiedAt: DateTime) = { Id = Guid.NewGuid() }


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

