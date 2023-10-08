namespace Carcard.Api.Models

open System

type Vehicle = {
    Id: Guid
    Vendor: string
    Model: string
    Year: DateTime
    MaintenanceHistory: option<list<Maintenance>>
}

module Vehicle =
    let add a = ()

