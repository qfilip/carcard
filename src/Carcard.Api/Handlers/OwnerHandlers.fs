module OwnerHandlers

open System
open System.Linq
open FsToolkit.ErrorHandling
open Microsoft.EntityFrameworkCore

open Carcard.Api.Dtos
open Carcard.Api.DataAccess
open Carcard.Api.Primitives
open Carcard.Api.Models
open Carcard.Database.Contexts
open Carcard.Database.Entities

open Utilities

let getAll () = task {
    let! owners =
        ()
        |> OwnerDb.getAllOwnersQuery
        |> DbUtils.execute

    return owners |> List.map OwnerDto.ofDbRecord
}

let getById (id: Guid) =
    id
    |> OwnerDb.getByIdQuery
    |> DbUtils.execute


let create (dto: OwnerDto) = taskResult {
    let! model = OwnerDto.toModel dto
    
    let! existingOwners =
        (model.Name)
        |> OwnerDb.getOwnerByNameQuery
        |> DbUtils.execute

    match existingOwners.Length with
    | 0 ->
        let dbRecord = {
            Model = model
            EntityData = EntityData.New
            EntityRelations = OwnerRelations
        }
        let! _ =
            dbRecord
            |> OwnerDb.getInsertOwnerCommand 
            |> DbUtils.execute
        return! Ok (dbRecord |> OwnerDto.ofDbRecord)
    | 1 ->
        return! Error (DomainError.Rejected "Owner with the same already exists")
    | _ ->
        return failwith (sprintf "More than one owner with name %s found" model.Name)
}