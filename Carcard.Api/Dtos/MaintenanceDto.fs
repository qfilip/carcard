namespace Carcard.Api.Dtos

open System
open Carcard.Api.Models

[<CLIMutable>]
type MaintenanceDto = {
    Repairman: string
    Date: DateTime
    Distance: int
    Description: string
    Cost: int
}

module MaintenanceDto =
    let toModel (dto: MaintenanceDto) = ()
        //Owner.Utils.create (dto.Name)

    let ofModel (model: Maintenance) =
        let dto: MaintenanceDto = {
            Repairman = model.Repairman
            Date = model.Date
            Distance = model.Distance
            Description = model.Description
            Cost = model.Cost
        }

        dto
