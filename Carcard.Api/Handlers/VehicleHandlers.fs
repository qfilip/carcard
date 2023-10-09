module VehicleHandlers

open FsToolkit.ErrorHandling
open Carcard.Api.Dtos
open Carcard.Api.DataAccess
open Carcard.Api.Primitives

let create (dto: VehicleDto) = taskResult {
    let! model = VehicleDto.toModel dto
    
    let! ownerDbr =
        dto.OwnerId
        |> OwnerDb.getByIdQuery
        |> DbUtils.execute

    let! ownerData = 
        ownerDbr
        |> DomainError.ofOptionMap (fun x -> x.EntityData) (Rejected "OwnerId not found")

    let dbRecord = {
        Model = model
        EntityData = EntityData.New
        EntityRelations = { OwnerId = ownerData.Id }
    }

    let! _ =
        dbRecord
        |> VehicleDb.getInsertVehicleCommand
        |> DbUtils.execute
    
    return VehicleDto.ofDbRecord dbRecord
}