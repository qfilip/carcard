namespace Carcard.Api.DataAccess

open System
open System.Data.Common
open System.Data.SQLite
open Carcard.Api.Models

module VehicleDb =
    let private mapDbRecord (rdr: DbDataReader) =
        ()
        let modelMapper () = Unchecked.defaultof<Vehicle>
            //Vendor = read m.Vendor |> rdr.GetString
            //Model = read m.Model |> rdr.GetString
            //Year = read m.Year |> rdr.GetDateTime
            //MaintenanceHistory = []


        let relationsMapper () = {
            OwnerId = DbUtils.read (nameof Unchecked.defaultof<VehicleRelations>.OwnerId) rdr.GetGuid rdr
        }

        DbUtils.mapDbRecord rdr modelMapper relationsMapper
        

    let getByIdQuery (id: Guid) =
        let formatter (cmd: SQLiteCommand) =
            cmd.CommandText <- "SELECT * FROM Vehicle WHERE Id = @Id"
            cmd.Parameters.AddWithValue("@Id", id.ToString()) |> ignore

        let operation (cmd: SQLiteCommand) = task {
            use! reader = cmd.ExecuteReaderAsync();
            
            match reader.Read() with
            | true -> return Some (mapDbRecord reader)
            | false -> return None
        }

        (formatter, operation)


    let getInsertVehicleCommand (x: VehicleDbRecord) =
        let formatter (cmd: SQLiteCommand) =
            let cmdText =
                """
                    insert into Vehicle(Id, Vendor, Model, Year)
                    values (@Id, @Vendor, @Model, @Year)
                """

            cmd.CommandText <- cmdText
            cmd.Parameters.AddWithValue("@Id", x.EntityData.Id.ToString()) |> ignore
            cmd.Parameters.AddWithValue("@OwnerId", x.EntityRelations.OwnerId.ToString()) |> ignore
            cmd.Parameters.AddWithValue("@Vendor", x.Model.Vendor) |> ignore
            cmd.Parameters.AddWithValue("@Model", x.Model.Model) |> ignore
            cmd.Parameters.AddWithValue("@Year", x.Model.Year) |> ignore

        let operation (cmd: SQLiteCommand) = task {
            return! cmd.ExecuteNonQueryAsync()
        }

        (formatter, operation)

