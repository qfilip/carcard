namespace Carcard.Api.Models

open Carcard.Api.Primitives
open Carcard.Api.ComputationExpressions

type Owner = {
    Name: String2
    Vehicles: Vehicle list
}

module Owner =
    let validate (name: string) (vehicles: Vehicle list) =
        ResultExpression() {
            let! name = name |> String2.ofString
            return { Name = name; Vehicles = vehicles }
        }

    type Utils() =
        static member create (name: string) =
            validate name []
        
        static member create (name: string, vehicles: Vehicle list) =
            validate name vehicles


