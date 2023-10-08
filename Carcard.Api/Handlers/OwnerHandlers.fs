module OwnerHandlers

open FsToolkit.ErrorHandling
open Carcard.Api.Dtos
open Carcard.Api.DataAccess
open Carcard.Api.Primitives

let getAll () = task {
    let! owners =
        ()
        |> OwnerDb.getAllOwnersQuery
        |> DbUtils.execute

    return owners |> List.map OwnerDto.ofModel
}


let insert (dto: OwnerDto) = taskResult {
    let! model = OwnerDto.toModel dto
    
    let! existingOwners =
        (model.Name)
        |> OwnerDb.getOwnerByNameQuery
        |> DbUtils.execute

    match existingOwners.Length with
    | 0 ->
        let! _ =
            model
            |> OwnerDb.getInsertOwnerCommand 
            |> DbUtils.execute
        return Ok model
    | 1 ->
        return Error (DomainError.Rejected "Owner with the same already exists")
    | _ ->
        return failwith (sprintf "More than one owner with name %s found" model.Name)
}