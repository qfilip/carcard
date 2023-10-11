namespace Carcard.Api.Models

open System
open Carcard.Api.Primitives
open Carcard.Api.ComputationExpressions
open Carcard.Database.Entities

type Maintenance = {
    Repairman: String2
    Date: DateTime
    Distance: Distance
    Description: String2
    Cost: MaintenanceCost
}

module Maintenance =
    let validate
        (repairman: string)
        (date: DateTime)
        (distance: int)
        (description: string)
        (cost: int) =
        ResultExpression() {
            let! repairman =    repairman   |> String2.ofString
            and! distance =     distance    |> Distance.ofInt
            and! description =  description |> String2.ofString
            and! cost =         cost        |>  MaintenanceCost.ofInt

            return {
                Repairman = repairman
                Date = date 
                Distance = distance
                Description = description
                Cost = cost
            }
        }
    
    let ofEntity (e: MaintenanceEntity) =
        validate
            e.Repairman
            e.Date
            e.Distance
            e.Description
            e.Cost