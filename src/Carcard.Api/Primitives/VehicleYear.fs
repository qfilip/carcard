namespace Carcard.Api.Primitives

open System

type VehicleYear = private VehicleYear of int

module Year =
    let from (x: int) (now: DateTime) =
        if x <= now.Year
        then Ok (VehicleYear x)
        else Error (DomainError.Validation ["Invalid vehicle year"])

    let raw (VehicleYear x) = x

