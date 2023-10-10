namespace Carcard.Api.Models

open System
open Carcard.Api.Primitives

type Maintenance = {
    Repairman: String2
    Date: DateTime
    Distance: Distance
    Description: String2
    Cost: MaintenanceCost
}