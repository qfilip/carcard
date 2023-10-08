namespace Carcard.Api.ComputationExpressions

open ExpressionUtils

type ResultExpression() =
    member this.Bind(x, f) = Result.bind f x
    member this.BindReturn(x, f) = Result.map f x
    member this.MergeSources(x, y) = zip x y
    member this.Return(x) = Ok x


