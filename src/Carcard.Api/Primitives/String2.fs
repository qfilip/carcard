namespace Carcard.Api.Primitives

type String2 = String2 of string

module String2 =
    let ofString (x: string) =
        match Validators.strLength 2 x with
        | Ok str -> Ok (String1 str)
        | Error e -> Error (DomainError.Validation [e])

    let ofProperty (x: string) (propName: string) =
        match Validators.strLength 1 x with
        | Ok str -> Ok (String2 str)
        | Error e -> Error (DomainError.Validation [sprintf "Property %s validation failed. Reason: %s" propName e])

