﻿namespace Carcard.Api.Models

open System
open Carcard.Api.Primitives
open Carcard.Api.ComputationExpressions
open Carcard.Database.Entities

type Owner = {
    Name: string
    Vehicles: Vehicle list
}

module Owner =
    let private validateName (name: string) =
        match name with
        | x when String.IsNullOrEmpty(x) ->
            Error (DomainError.Validation ["Owner name cannot be empty"])
        | x when x.Length < 2 ->
            Error (DomainError.Validation ["Owner name must be at least 2 characters long"])
        | x -> Ok x


    let private validate (name: string) (vehicles: Vehicle list) =
        let exp = ResultExpression()
        exp {
            let! oname = validateName name
            return { Name = oname; Vehicles = vehicles }
        }

    let ofEntity (x: OwnerEntity) =
        let exp = ResultExpression()
        exp {
            let! name = x.Name |> validateName
            // and! vhs = x.Vehicles |> List.map (fun x -> x)
            return { Name = name; Vehicles = [] }
        }

    type Utils() =
        static member create (name: string) =
            validate name []
        
        static member create (name: string, vehicles: Vehicle list) =
            validate name vehicles

