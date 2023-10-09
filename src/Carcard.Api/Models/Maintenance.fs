namespace Carcard.Api.Models

open System

type Maintenance = {
    Id: Guid
    Repairman: string
    Date: DateTime
    Distance: int
    Description: string
    Cost: int
}

