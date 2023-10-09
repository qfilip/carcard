namespace Carcard.Api.Dtos

open Carcard.Api.Models
open Carcard.Api.DataAccess

[<CLIMutable>]
type OwnerDto = {
    Name: string
    Vehicles: VehicleDto list
    EntityData: EntityData
} with
    static member Default = {
        Name = null
        Vehicles = []
        EntityData = EntityData.Empty
    }


module OwnerDto =
    let toModel (dto: OwnerDto) =
        Owner.Utils.create (dto.Name)


    let ofModel (model: Owner) =
         { OwnerDto.Default with
            Name = model.Name
            Vehicles = model.Vehicles |> List.map VehicleDto.ofModel
            EntityData = EntityData.Empty
         }


    let ofDbRecord (dbr: OwnerDbRecord) =
        { OwnerDto.Default with
            Name = dbr.Model.Name
            Vehicles = dbr.Model.Vehicles |> List.map VehicleDto.ofModel
            EntityData = dbr.EntityData
        }

