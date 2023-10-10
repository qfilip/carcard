namespace Carcard.Api.Primitives

type String1 = private String1 of string

module String1 =
    let private minLenght = 1

    let ofString (x: string) =
        match Validators.strLength minLenght x with
        | Ok str -> Ok (String1 str)
        | Error e -> Error [DomainError.Validation [e]]

    let ofProperty (x: string) (propName: string) =
        match Validators.strLength minLenght x with
        | Ok str -> Ok (String1 str)
        | Error e -> Error [DomainError.Validation [sprintf "Property %s validation failed. Reason: %s" propName e]]

    let raw (String1 x) = x