namespace Carcard.Api.DataAccess

open System
open System.Data.SQLite
open System.Data.Common
open Carcard.Api.Models

type OwnerRelations = OwnerRelations

type OwnerDbRecord = DbRecord<Owner, OwnerRelations>

module OwnerDb =
    let mapDbRecord (rdr: DbDataReader) =
        let modelMapper () = {
            Name = DbUtils.read (nameof Unchecked.defaultof<Owner>.Name) rdr.GetString rdr
            Vehicles = []
        }

        let relationsMapper () = OwnerRelations

        DbUtils.mapDbRecord rdr modelMapper relationsMapper


    let getAllOwnersQuery () =
        let formatter (cmd: SQLiteCommand) =
            cmd.CommandText <- "SELECT * FROM Owner"

        let operation (cmd: SQLiteCommand) = task {
            use! reader = cmd.ExecuteReaderAsync();
            
            let mutable records: OwnerDbRecord list = []
            while reader.Read() do
                records <- (mapDbRecord reader)::records
            
            return records
        }

        (formatter, operation)


    let getByIdQuery (id: Guid) =
        let formatter (cmd: SQLiteCommand) =
            cmd.CommandText <- "SELECT * FROM Owner WHERE Id = @Id"
            cmd.Parameters.AddWithValue("@Id", id.ToString()) |> ignore

        let operation (cmd: SQLiteCommand) = task {
            use! reader = cmd.ExecuteReaderAsync();
            
            match reader.Read() with
            | true -> return Some (mapDbRecord reader)
            | false -> return None
        }

        (formatter, operation)
    

    let getOwnerByNameQuery (name: string) =
        let formatter (cmd: SQLiteCommand) =
            cmd.CommandText <- "SELECT * FROM Owner WHERE Name = @Name"
            cmd.Parameters.AddWithValue("@Name", name) |> ignore

        let operation (cmd: SQLiteCommand) = task {
            use! reader = cmd.ExecuteReaderAsync();
            
            let mutable records: OwnerDbRecord list = []
            while reader.Read() do
                records <- (mapDbRecord reader)::records
            
            return records
        }

        (formatter, operation)


    let getInsertOwnerCommand (x: OwnerDbRecord) =
        let formatter (cmd: SQLiteCommand) =
            let cmdText =
                """
                    insert into Owner(Id, Name)
                    values (@Id, @Name)
                """

            cmd.CommandText <- cmdText
            cmd.Parameters.AddWithValue("@Id", x.EntityData.Id.ToString()) |> ignore
            cmd.Parameters.AddWithValue("@Name", x.Model.Name) |> ignore

        let operation (cmd: SQLiteCommand) = task {
            return! cmd.ExecuteNonQueryAsync()
        }

        (formatter, operation)

