namespace Carcard.Api

#nowarn "20"

open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting

module Program =
    let exitCode = 0

    [<EntryPoint>]
    let main args =

        let builder = WebApplication.CreateBuilder(args)

        Startup.runStartupConfig builder.Environment

        let app = builder.Build()

        Endpoints.OwnerEndpoints.map app
        
        app.UseHttpsRedirection()

        app.Run()

        exitCode
