namespace Carcard.Api.DataAccess

open System
open Carcard.Api.Models
open Carcard.Database.Entities
open Carcard.Api.ComputationExpressions
open Carcard.Api.Primitives

module MaintenanceDb =
    let ofEntity (e: MaintenanceEntity) =
        ResultExpression() {
            let! model =
                Maintenance.validate
                    e.Repairman
                    e.Date
                    e.Distance
                    e.Description
                    e.Cost
            
            let entityData = EntityData.ofBaseEntity e
            
            return {
                Model = model
                EntityData = entityData
                EntityRelations = MaintenanceRelations.ofEntity e
            }
        }

    let toEntity (x: MaintenanceDbRecord) =
        let e = MaintenanceEntity()
        
        EntityData.populateBaseEntity e x.EntityData
        MaintenanceRelations.populateRelations e x.EntityRelations
        
        e.Repairman     <- x.Model.Repairman    |> String2.raw
        e.Date          <- x.Model.Date
        e.Distance      <- x.Model.Distance     |> Distance.raw
        e.Description   <- x.Model.Description  |> String2.raw
        e.Cost          <- x.Model.Cost         |> MaintenanceCost.raw