module OwnerHandlers

open FsToolkit.ErrorHandling
open Carcard.Api.Dtos
open Carcard.Api.DataAccess

let getAll () =
    let fao = OwnerDb.getAllOwnersQuery ()
    DbUtils.execute fao


let insert (dto: OwnerDto) = taskResult {
    let! model = OwnerDto.toModel dto
    let fao = OwnerDb.getInsertOwnerCommand model
    let! _ = DbUtils.execute fao
    
    return model
}