namespace Carcard.Api.Primitives

open System

type Year = private Year of int

module Year =
    let from (x: int) (now: DateTime) = Year x

