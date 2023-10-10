namespace Carcard.Api.Primitives

type String2 = private String2 of string

module String2 =
    let private minLenght = 2

    let ofString (x: string) =
        match Validators.strLength minLenght x with
        | Ok str -> Ok (String2 str)
        | Error e -> Error [DomainError.Validation [e]]

    let ofProperty (x: string) (propName: string) =
        match Validators.strLength minLenght x with
        | Ok str -> Ok (String2 str)
        | Error e -> Error [DomainError.Validation [sprintf "Property %s validation failed. Reason: %s" propName e]]

    let raw (String2 x) = x

