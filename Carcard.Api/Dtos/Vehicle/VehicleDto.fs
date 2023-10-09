namespace Carcard.Api.Dtos

open System
open Carcard.Api.DataAccess
open Carcard.Api.Models

[<CLIMutable>]
type VehicleDto = {
    Vendor: string
    Model: string
    Year: DateTime
    MaintenanceHistory: MaintenanceDto list
    EntityData: EntityData
    EntityRelations: VehicleRelations
} with
    static member Default = {
        Vendor = null
        Model = null
        Year = DateTime.MinValue
        MaintenanceHistory = []
        EntityData = EntityData.Empty
        EntityRelations = VehicleRelations.Empty
    }

module VehicleDto =
    let toModel (dto: VehicleDto) =
        Vehicle.Utils.create (dto.Vendor, dto.Model, dto.Year)
    

    let ofModel (model: Vehicle) =
        let dto: VehicleDto = {
            Vendor = model.Vendor
            Model = model.Model
            Year = model.Year
            MaintenanceHistory = model.MaintenanceHistory |> List.map MaintenanceDto.ofModel
            EntityData = EntityData.Empty
            EntityRelations = VehicleRelations.Empty
        }

        dto


    let ofDbRecord (dr: VehicleDbRecord) =
        let dto: VehicleDto = {
            Vendor = dr.Model.Vendor
            Model = dr.Model.Model
            Year = dr.Model.Year
            MaintenanceHistory = dr.Model.MaintenanceHistory |> List.map MaintenanceDto.ofModel
            EntityData = dr.EntityData
            EntityRelations = dr.EntityRelations
        }

        dto

