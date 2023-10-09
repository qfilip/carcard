namespace Carcard.Api.Primitives

type DomainError =
| Validation of string list
| Rejected of string
| NotFound of string

module DomainError =
    let ofOption (opt: 'a option) (errorType: DomainError) =
        match opt with
        | Some x -> Ok x
        | None -> Error errorType

