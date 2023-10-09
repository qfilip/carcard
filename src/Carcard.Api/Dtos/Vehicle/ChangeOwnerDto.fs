namespace Carcard.Api.Dtos

open System

type ChangeOwnerDto = {
    VehicleId: Guid
    NewOwnerId: Guid
}

