namespace Carcard.Api.Models

open System

type Maintenance = {
    Repairman: string
    Date: DateTime
    Distance: int
    Description: string
    Cost: int
}

