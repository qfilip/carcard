namespace Carcard.Api.Dtos

open System
open Carcard.Api.Models
open Carcard.Api.Primitives

[<CLIMutable>]
type MaintenanceDto = {
    Repairman: string
    Date: DateTime
    Distance: int
    Description: string
    Cost: int
}

module MaintenanceDto =
    let toModel (dto: MaintenanceDto) =
        Maintenance.validate
            dto.Repairman
            dto.Date
            dto.Distance
            dto.Description
            dto.Cost


    let ofModel (model: Maintenance): MaintenanceDto =
        {
            Repairman =     model.Repairman     |> String2.raw
            Date =          model.Date
            Distance =      model.Distance      |> Distance.raw
            Description =   model.Description   |> String2.raw 
            Cost =          model.Cost          |> MaintenanceCost.raw
        }
