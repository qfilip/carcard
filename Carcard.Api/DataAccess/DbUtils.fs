namespace Carcard.Api.DataAccess

open System.Data.Common
open System.Data.SQLite
open System.Threading.Tasks

module DbUtils =
    let read (property: string) (readOrdinal: int -> 'a) (reader: DbDataReader) =
        property
        |> reader.GetOrdinal
        |> readOrdinal


    let execute (formatter: SQLiteCommand -> unit, operation: SQLiteCommand -> Task<'a>) = task {
        use conn = new SQLiteConnection(Startup.getDbConnectionString ())
        use cmd = new SQLiteCommand(conn)
        do formatter cmd
        
        do! conn.OpenAsync()
        let! result = operation cmd
        do! conn.CloseAsync()

        return result
    }

