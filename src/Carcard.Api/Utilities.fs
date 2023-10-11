
module Utilities
open System.Linq.Expressions
open Microsoft.FSharp.Linq.RuntimeHelpers

    module Option =
        let defaultList (mapper: 'a -> 'b) (x: option<list<'a>>) =
            x
            |> Option.map (List.map mapper)
            |> Option.defaultWith (fun _ -> [])

        let ofNull (x: 'a) = if x = null then Some x else None

        let toNull = function | Some x -> x | None -> null
    
    module Result =
        let allOk (rs: seq<Result<'a, 'b>>) =
            rs
            |> Seq.forall (function | Ok _ -> true | Error _ -> false)
            |> fun noErrors ->
                match noErrors with
                | true ->
                    rs
                    |> Seq.fold (fun acc x ->
                        match x with
                        | Ok v -> v::acc
                        | Error _ -> acc
                    ) []
                | false ->
                    rs
                    |> Seq.fold (fun acc x ->
                        match x with
                        | Ok _ -> acc
                        | Error es -> es::acc
                    ) []

    module Lambda =
        let toExpression (``f# lambda`` : Quotations.Expr<'a>) =
            ``f# lambda``
            |> LeafExpressionConverter.QuotationToExpression 
            |> unbox<Expression<'a>>

        // <@ Func<int, int>(fun i -> i + 5) @>


