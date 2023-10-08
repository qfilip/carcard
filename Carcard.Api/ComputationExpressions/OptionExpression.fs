namespace Carcard.Api.ComputationExpressions

type OptionExpression() =
    member this.Bind(x, f) = Option.bind f x
    member this.Return(x) = Some x