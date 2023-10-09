namespace Carcard.Api.Endpoints

open Microsoft.AspNetCore.Builder
open Carcard.Api.Endpoints.V1_0

module OwnerEndpoints =
    let map (app: WebApplication) =
        OwnerEndpointsV1_0.map app

