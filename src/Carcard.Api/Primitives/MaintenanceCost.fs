namespace Carcard.Api.Primitives

type MaintenanceCost = private MaintenanceCost of int

module MaintenanceCost =
    let ofInt (x: int) =
        match x with
        | d when d < 0 -> Error [DomainError.Validation ["Maintenance cost cannot be less than 0"]]
        | _ -> Ok (MaintenanceCost x)


    let raw (MaintenanceCost x) = x