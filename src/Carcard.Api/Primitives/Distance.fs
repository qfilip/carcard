namespace Carcard.Api.Primitives

type Distance = private Distance of int

module Distance =
    let ofInt (x: int) =
        match x with
        | d when d < 0 -> Error [DomainError.Validation ["Distance cannot be less than 0"]]
        | d when d > 1_000_000 -> Error [DomainError.Validation ["I don't believe that the vehicle is still functioning after 1.000.000 km/miles crossed"]]
        | _ -> Ok (Distance x)

    let raw (Distance x) = x