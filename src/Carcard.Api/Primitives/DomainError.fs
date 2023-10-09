namespace Carcard.Api.Primitives

type DomainError =
| Validation of string list
| Rejected of string
| NotFound of string

module DomainError =
    let ofOptionMap (mapper: 'a -> 'b) (domainError: DomainError) (opt: 'a option) =
        match opt with
        | Some x -> Ok (mapper x)
        | None -> Error domainError


    let ofOption (domainError: DomainError) (opt: 'a option) =
        match opt with
        | Some x -> Ok x
        | None -> Error domainError

