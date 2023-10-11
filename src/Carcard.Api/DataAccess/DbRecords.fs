namespace Carcard.Api.DataAccess

open System
open Carcard.Api.Models
open Carcard.Database.Entities

type OwnerRelations = OwnerRelations
type OwnerDbRecord = DbRecord<Owner, OwnerRelations>

type VehicleRelations = {
    OwnerId: Guid
    Owner: OwnerDbRecord option
} with
    static member Empty = {
        OwnerId = Guid.Empty
        Owner = None
    }

type VehicleDbRecord = DbRecord<Vehicle, VehicleRelations>


type MaintenanceRelations = {
    VehicleId: Guid
    Vehicle: VehicleDbRecord option
}
module MaintenanceRelations =
    let ofEntity (e: MaintenanceEntity) =
        {
            VehicleId = e.VehicleId
            Vehicle = None
        }

    let populateRelations (e: MaintenanceEntity) (r: MaintenanceRelations) =
        e.VehicleId <- r.VehicleId

type MaintenanceDbRecord = DbRecord<Maintenance, MaintenanceRelations>


