namespace Carcard.Api.Endpoints

open Microsoft.AspNetCore.Builder
open Carcard.Api.Endpoints.V1_0

module VehicleEndpoints =
    let map (app: WebApplication) =
        VehicleEndpointsV1_0.map app
