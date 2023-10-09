namespace Carcard.Api.Endpoints.V1_0

#nowarn "20"

open System
open System.Threading.Tasks
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http
open Carcard.Api.Dtos

module VehicleEndpointsV1_0 =
    let private create =
        Func<VehicleDto, Task<IResult>>(fun dto -> task {
            let! result = VehicleHandlers.create dto
            return EndpointUtils.mapResult (Results.Ok) result
        })


    let private changeOwner =
        Func<VehicleDto, Task<IResult>>(fun dto -> task {
            let! result = VehicleHandlers.create dto
            return EndpointUtils.mapResult (Results.Ok) result
        })


    let map (app: WebApplication) =
        let group = app.MapGroup("v1.0/vehicles")
        
        group.MapPost("/create", create)
        group.MapPost("/change-owner", changeOwner)
        ()
