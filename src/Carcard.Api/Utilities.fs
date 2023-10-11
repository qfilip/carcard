
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
    
    
    module Lambda =
        let toExpression (``f# lambda`` : Quotations.Expr<'a>) =
            ``f# lambda``
            |> LeafExpressionConverter.QuotationToExpression 
            |> unbox<Expression<'a>>

        // <@ Func<int, int>(fun i -> i + 5) @>


