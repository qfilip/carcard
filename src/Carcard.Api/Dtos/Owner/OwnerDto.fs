namespace Carcard.Api.Dtos

open System
open Carcard.Api.Models
open Carcard.Api.DataAccess

[<CLIMutable>]
type OwnerDto = {
    Id: Guid
    Name: string
    Vehicles: VehicleDto list
} with
    static member Default = {
        Id = Guid.Empty
        Name = ""
        Vehicles = []
    }


module OwnerDto =
    let toModel (dto: OwnerDto) =
        Owner.Utils.create (dto.Name)


    let ofModel (model: Owner) =
         { OwnerDto.Default with
            Name = model.Name
            Vehicles = model.Vehicles |> List.map VehicleDto.ofModel
         }


    let ofDbRecord (dbr: OwnerDbRecord) =
        let dto: OwnerDto = { 
            Id = dbr.EntityData.Id
            Name = dbr.Model.Name
            Vehicles = dbr.Model.Vehicles |> List.map VehicleDto.ofModel
        }

        dto

