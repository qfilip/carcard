namespace Carcard.Api.Models

open System
open Carcard.Api.Primitives
open Carcard.Api.ComputationExpressions

type Owner = {
    Name: string
    Vehicles: option<list<Vehicle>>
}

module Owner =
    let private validateName (name: string) =
        match name with
        | x when String.IsNullOrEmpty(x) ->
            Error (DomainError.Validation ["Owner name cannot be empty"])
        | x when x.Length < 2 ->
            Error (DomainError.Validation ["Owner name must be at least 2 characters long"])
        | x -> Ok x


    let validate (name: string) (vehicles: option<list<Vehicle>>) =
        let exp = ResultExpression()
        exp {
            let! oname = validateName name
            return { Name = oname; Vehicles = vehicles }
        }

    type Utils() =
        static member create (name: string) =
            validate name None

        static member create (name: string, vehicles: option<list<Vehicle>>) =
            validate name vehicles


