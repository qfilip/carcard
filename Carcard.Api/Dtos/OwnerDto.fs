namespace Carcard.Api.Dtos

open Carcard.Api.Models
open Carcard.Api.DataAccess

[<CLIMutable>]
type OwnerDto = {
    Name: string
    EntityData: EntityData
}

module OwnerDto =
    let toModel (dto: OwnerDto) =
        Owner.Utils.create (dto.Name)

    let ofModel (model: Owner) =
        { Name = model.Name; EntityData = EntityData.empty }

    let ofDbRecord (dbr: DbRecord<Owner>) =
        { Name = dbr.Record.Name; EntityData = dbr.EntityData }

