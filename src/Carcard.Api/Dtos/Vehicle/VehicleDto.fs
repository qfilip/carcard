namespace Carcard.Api.Dtos

open System
open Carcard.Api.DataAccess
open Carcard.Api.Models

[<CLIMutable>]
type VehicleDto = {
    Id: Guid
    OwnerId: Guid
    Vendor: string
    Model: string
    Year: DateTime
    MaintenanceHistory: MaintenanceDto list
} with
    static member Default = {
        Id = Guid.Empty
        OwnerId = Guid.Empty
        Vendor = ""
        Model = ""
        Year = DateTime.MinValue
        MaintenanceHistory = []
    }

module VehicleDto =
    let toModel (dto: VehicleDto) =
        Vehicle.Utils.create (dto.Vendor, dto.Model, dto.Year)
    

    let ofModel (model: Vehicle) =
        { VehicleDto.Default with
            Vendor = model.Vendor
            Model = model.Model
            Year = model.Year
            MaintenanceHistory = model.MaintenanceHistory |> List.map MaintenanceDto.ofModel
        }


    let ofDbRecord (dbr: VehicleDbRecord) =
        let dto: VehicleDto = {
            Id = dbr.EntityData.Id
            OwnerId = dbr.EntityRelations.OwnerId
            Vendor = dbr.Model.Vendor
            Model = dbr.Model.Model
            Year = dbr.Model.Year
            MaintenanceHistory = dbr.Model.MaintenanceHistory |> List.map MaintenanceDto.ofModel
        }

        dto

