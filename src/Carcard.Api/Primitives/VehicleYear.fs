namespace Carcard.Api.Primitives

open System

type VehicleYear = private VehicleYear of int

module VehicleYear =
    let ofInt (now: DateTime) (x: int) =
        match x with
        | y when y < 1885 -> Error [DomainError.Validation ["Vehicle year cannot be less than 1885"]]
        | y when y <= now.Year -> Error [DomainError.Validation ["Vehicle year cannot be in the future"]]
        | y -> Ok (VehicleYear y)


    let raw (VehicleYear x) = x

