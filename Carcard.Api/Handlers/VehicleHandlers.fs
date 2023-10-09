module VehicleHandlers

open FsToolkit.ErrorHandling
open Carcard.Api.Dtos
open Carcard.Api.DataAccess

let create (dto: VehicleDto) = taskResult {
    let! model = VehicleDto.toModel dto
    
    let! ownerDbr =
        dto.EntityRelations.OwnerId
        |> OwnerDb.getByIdQuery
        |> DbUtils.execute

    //let! owner = 
    let dbRecord = {
        Model = model
        EntityData = EntityData.create ()
        EntityRelations = { OwnerId = dto.EntityRelations.OwnerId }
    }

    let! _ =
        dbRecord
        |> VehicleDb.getInsertVehicleCommand
        |> DbUtils.execute
    
    return VehicleDto.ofDbRecord dbRecord
}