namespace Carcard.Api.Primitives

open System

module Validators =
    let strLength (requiredLength: int) (str: string) =
        let getError () = Error (sprintf "Minimum required length is %i" requiredLength)
        
        match str with
        | x when String.IsNullOrEmpty(x) -> getError ()
        | x when x.Length < requiredLength -> getError ()
        | x -> Ok x

