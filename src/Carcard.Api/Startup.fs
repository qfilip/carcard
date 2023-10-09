module Startup

open Microsoft.AspNetCore.Hosting
open System

let mutable private dbConnectionString: string option = None

let private setDbConnectionString (env: IWebHostEnvironment) =
    let dbPath = IO.Path.Combine(env.WebRootPath, "database")
    dbConnectionString <- Some (sprintf "Data Source=%s;Version=3" dbPath)

let getDbConnectionString () =
    match dbConnectionString with
    | Some x -> x
    | None -> failwith "Db connection string not set"

let runStartupConfig (env: IWebHostEnvironment) =
    setDbConnectionString env