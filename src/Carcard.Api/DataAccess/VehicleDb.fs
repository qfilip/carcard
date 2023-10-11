namespace Carcard.Api.DataAccess

open System
open System.Data.Common
open System.Data.SQLite
open Carcard.Api.Models
open Carcard.Database.Entities
open Carcard.Api.ComputationExpressions


type VehicleRelations = {
    OwnerId: Guid
} with
    static member Empty = {
        OwnerId = Guid.Empty
    }

type VehicleDbRecord = DbRecord<Vehicle, VehicleRelations>


module VehicleDb =
    let ofEntity (e: VehicleEntity) =
        let maintenances = e.Maintenances |> Seq.map Maintenance.ofEntity
        //ResultExpression() {
        //    let! model =
        //        Vehicle.validate
        //            e.Vendor
        //            e.Model
        //            e.Year
        //            maintenances

        //    return model
        //}
        ()

