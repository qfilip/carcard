namespace Carcard.Api

open Carcard.Api.Dtos
open System
open Carcard.Database.Contexts

#nowarn "20"

open Microsoft.Extensions.DependencyInjection
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.EntityFrameworkCore

module Program =
    let exitCode = 0

    [<EntryPoint>]
    let main args =
        let builder = WebApplication.CreateBuilder(args)

        Startup.runStartupConfig builder.Environment
        
        builder.Services.AddDbContext<AppDbContext>(fun cfg ->
            cfg.UseSqlite(Startup.getDbConnectionString ()) |> ignore)

        let app = builder.Build()

        Endpoints.OwnerEndpoints.map app
        Endpoints.VehicleEndpoints.map app
        
        app.UseHttpsRedirection()

        app.Run()

        exitCode
