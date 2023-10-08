namespace Carcard.Api.Dtos

open System
open Carcard.Api.Models
open Utilities

[<CLIMutable>]
type VehicleDto = {
    Id: Guid
    Vendor: string
    Model: string
    Year: DateTime
    MaintenanceHistory: MaintenanceDto list
}

module VehicleDto =
    let toModel (dto: VehicleDto) = ()
        //Owner.Utils.create (dto.Name)

    let ofModel (model: Vehicle) =
        let dto: VehicleDto = {
            Id = model.Id
            Vendor = model.Vendor
            Model = model.Model
            Year = model.Year
            MaintenanceHistory = Option.defaultList MaintenanceDto.ofModel model.MaintenanceHistory
        }

        dto

