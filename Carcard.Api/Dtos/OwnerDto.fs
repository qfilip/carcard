namespace Carcard.Api.Dtos

open System
open Carcard.Api.Models

[<CLIMutable>]
type OwnerDto = {
    Id: Guid
    Name: string
}

module OwnerDto =
    let toModel (dto: OwnerDto) =
        Owner.Utils.create (Guid.NewGuid(), dto.Name, None)

