module OwnerHandlers

open System
open FsToolkit.ErrorHandling
open Carcard.Api.Dtos
open Carcard.Api.DataAccess
open Carcard.Api.Primitives
open Carcard.Api.Models

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
            EntityData = EntityData.Empty
            EntityRelations = OwnerRelations
        }
        let! _ =
            dbRecord
            |> OwnerDb.getInsertOwnerCommand 
            |> DbUtils.execute
        return Ok (model |> OwnerDto.ofModel)
    | 1 ->
        return Error (DomainError.Rejected "Owner with the same already exists")
    | _ ->
        return failwith (sprintf "More than one owner with name %s found" model.Name)
}