namespace Carcard.Api.Dtos

open System
open Carcard.Api.DataAccess
open Carcard.Api.Models
open Carcard.Api.Primitives

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
            Vendor =    model.Vendor    |> String1.raw
            Model =     model.Model     |> String1.raw
            Year =      model.Year
            MaintenanceHistory =
                        model.MaintenanceHistory
                        |> List.map MaintenanceDto.ofModel
        }


    let ofDbRecord (dbr: VehicleDbRecord) =
        let dto: VehicleDto = {
            Id = dbr.EntityData.Id
            OwnerId =   dbr.EntityRelations.OwnerId
            Vendor =    dbr.Model.Vendor    |> String1.raw
            Model =     dbr.Model.Model     |> String1.raw
            Year =      dbr.Model.Year
            MaintenanceHistory =
                dbr.Model.MaintenanceHistory
                |> List.map MaintenanceDto.ofModel
        }

        dto

