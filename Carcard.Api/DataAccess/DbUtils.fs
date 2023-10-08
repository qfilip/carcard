namespace Carcard.Api.DataAccess

open System
open System.Data.Common
open System.Data.SQLite
open System.Threading.Tasks

type EntityData = {
    Id: Guid
    //CreatedAt: DateTime
    //ModifiedAt: DateTime
}

module EntityData =
    let empty = { Id = Guid.Empty }

    let create () = { Id = Guid.NewGuid() }


type DbRecord<'a> = {
    Record: 'a
    EntityData: EntityData
}

module DbUtils =
    let read (property: string) (readOrdinal: int -> 'a) (reader: DbDataReader) =
        property
        |> reader.GetOrdinal
        |> readOrdinal


    let mapDbRecord (rdr: DbDataReader) (mapper: unit -> 'a) =
        let dbRecord = mapper ()
        let entityData = {
            Id = read (nameof Unchecked.defaultof<EntityData>.Id) rdr.GetGuid rdr
        }

        {
            Record = dbRecord
            EntityData = entityData
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

