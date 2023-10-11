namespace Carcard.Api.Models

open System
open Carcard.Api.Primitives
open Carcard.Api.ComputationExpressions

type Vehicle = {
    Vendor: String1
    Model: String1
    Year: VehicleYear
    MaintenanceHistory: Maintenance list
}

module Vehicle =
    let validate
        (vendor: string)
        (model: string)
        (year: int)
        (maintenanceHistory: Maintenance list) =
        ResultExpression() {
            let! vendor =   vendor  |> String1.ofString
            and! model =    model   |> String1.ofString 
            and! year =     year    |> VehicleYear.ofInt DateTime.UtcNow
            
            return { 
                Vendor = vendor;
                Model = model;
                Year = year;
                MaintenanceHistory = maintenanceHistory
            }
        }

    let toEntity (x: Vehicle) =
        

    type Utils() =
        static member create (vendor: string, model: string, year: int) =
            validate vendor model year []

        static member create (vendor: string, model: string, year: int, maintenanceHistory: Maintenance list) =
            validate vendor model year maintenanceHistory

