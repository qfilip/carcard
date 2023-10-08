module EndpointUtils

open Microsoft.AspNetCore.Http
open Carcard.Api.Primitives

    let mapResult (onOk: 'a -> IResult) (result: Result<'a, DomainError>) =
        match result with
        | Ok x -> onOk x
        | Error de ->
            match de with
            | Validation e -> Results.BadRequest e
            | Rejected e -> Results.Conflict e
