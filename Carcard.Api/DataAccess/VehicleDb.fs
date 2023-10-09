namespace Carcard.Api.DataAccess

open System
open System.Data.SQLite
open Carcard.Api.Models

type VehicleRelations = {
    OwnerId: Guid
} with
    static member Empty = {
        OwnerId = Guid.Empty
    }

type VehicleDbRecord = DbRecord<Vehicle, VehicleRelations>

module VehicleDb =
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

