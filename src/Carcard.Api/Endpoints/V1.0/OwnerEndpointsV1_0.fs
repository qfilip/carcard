namespace Carcard.Api.Endpoints.V1_0

open Carcard.Database.Contexts

#nowarn "20"

open System
open System.Threading.Tasks
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http
open Carcard.Api.Dtos

module OwnerEndpointsV1_0 =
    let private getAll =
        Func<Task<IResult>>(fun () -> task {
            let! result = OwnerHandlers.getAll ()
            return EndpointUtils.mapResult (Results.Ok) (Ok result)
        })

    
    let private getById =
        Func<Guid, AppDbContext, Task<IResult>>(fun id dbContext ->
            
        )


    let private create =
        Func<OwnerDto, Task<IResult>>(fun dto -> task {
            let! result = OwnerHandlers.create dto
            return EndpointUtils.mapResult (Results.Ok) result
        })


    let map (app: WebApplication) =
        let group = app.MapGroup("v1.0/owners")

        group.MapGet("", getAll)
        group.MapPost("/create", create)

        ()
    
    

