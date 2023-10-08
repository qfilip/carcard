namespace Carcard.Api.DataAccess

open System.Data.SQLite
open System.Data.Common
open Carcard.Api.Models

module OwnerDb =
    let mapOwner (rdr: DbDataReader) =
        {
            Id = DbUtils.read (nameof Unchecked.defaultof<Owner>.Id) rdr.GetGuid rdr
            Name = DbUtils.read (nameof Unchecked.defaultof<Owner>.Name) rdr.GetString rdr
            Vehicles = None
        }

    let getAllOwnersQuery () =
        let formatter (cmd: SQLiteCommand) =
            cmd.CommandText <- "SELECT * FROM Owner"

        let operation (cmd: SQLiteCommand) = task {
            use! reader = cmd.ExecuteReaderAsync();
            
            let mutable records: Owner list = []
            while reader.Read() do
                records <- (mapOwner reader)::records
            
            return records
        }

        (formatter, operation)
    

    let getOwnerByNameQuery (name: string) =
        let formatter (cmd: SQLiteCommand) =
            cmd.CommandText <- "SELECT * FROM Owner WHERE Name = @Name"
            cmd.Parameters.AddWithValue("@Name", name) |> ignore

        let operation (cmd: SQLiteCommand) = task {
            use! reader = cmd.ExecuteReaderAsync();
            
            let mutable records: Owner list = []
            while reader.Read() do
                records <- (mapOwner reader)::records
            
            return records
        }

        (formatter, operation)


    let getInsertOwnerCommand (x: Owner) =
        let formatter (cmd: SQLiteCommand) =
            let cmdText =
                """
                    insert into Owner(Id, Name)
                    values (@Id, @Name)
                """

            cmd.CommandText <- cmdText
            cmd.Parameters.AddWithValue("@Id", x.Id.ToString()) |> ignore
            cmd.Parameters.AddWithValue("@Name", x.Name) |> ignore

        let operation (cmd: SQLiteCommand) = task {
            return! cmd.ExecuteNonQueryAsync()
        }

        (formatter, operation)

