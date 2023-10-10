namespace Carcard.Api.Models

open System
open Carcard.Api.Primitives
open Carcard.Api.ComputationExpressions

type Vehicle = {
    Vendor: String1
    Model: String1
    Year: DateTime
    MaintenanceHistory: Maintenance list
}

module Vehicle =
    let validate
        (vendor: string)
        (model: string)
        (year: DateTime)
        (maintenanceHistory: Maintenance list) =
        let exp = ResultExpression()
        exp {
            let! vendor = Validators.validateLength vendor 2 "Vendor"
            let! model = Validators.validateLength model 2 "Model"
            
            return { 
                Vendor = vendor;
                Model = model;
                Year = year;
                MaintenanceHistory = maintenanceHistory
            }
        }

    type Utils() =
        static member create (vendor: string, model: string, year: DateTime) =
            validate vendor model year []

        static member create (vendor: string, model: string, year: DateTime, maintenanceHistory: Maintenance list) =
            validate vendor model year maintenanceHistory

