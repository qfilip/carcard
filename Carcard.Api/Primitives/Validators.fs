namespace Carcard.Api.Primitives

open System

module Validators =
    let validateLength (str: string) (requiredLength: int) (propertyName: string) =
        let getErrorMessage () =
            sprintf "Property %s must be at least %i characters long" propertyName requiredLength
        
        match str with
        | x when String.IsNullOrEmpty(x) ->
            Error (DomainError.Validation [getErrorMessage ()])
        | x when x.Length < requiredLength ->
            Error (DomainError.Validation [getErrorMessage ()])
        | x -> Ok x

