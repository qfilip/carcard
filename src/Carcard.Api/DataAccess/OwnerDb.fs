namespace Carcard.Api.DataAccess

open System
open System.Linq
open System.Data.SQLite
open System.Data.Common
open Microsoft.EntityFrameworkCore
open Carcard.Api.Models
open Carcard.Database.Entities
open Carcard.Database.Contexts
open Utilities
open FsToolkit.ErrorHandling

type OwnerRelations = OwnerRelations
type OwnerDbRecord = DbRecord<Owner, OwnerRelations>

module OwnerDbRecord =
    let ofEntity (x: OwnerEntity) =
        x
        ()

module OwnerDb =
    let mapDbRecord (rdr: DbDataReader) =
        let modelMapper () = {
            Name = DbUtils.read (nameof Unchecked.defaultof<Owner>.Name) rdr.GetString rdr
            Vehicles = []
        }

        let relationsMapper () = OwnerRelations

        DbUtils.mapDbRecord rdr modelMapper relationsMapper


    let getAllOwnersQuery () =
        let formatter (cmd: SQLiteCommand) =
            cmd.CommandText <- "SELECT * FROM Owner"

        let operation (cmd: SQLiteCommand) = task {
            use! reader = cmd.ExecuteReaderAsync();
            
            let mutable records: OwnerDbRecord list = []
            while reader.Read() do
                records <- (mapDbRecord reader)::records
            
            return records
        }

        (formatter, operation)


    let getByIdQuery (id: Guid) (ctx: AppDbContext) = task {
        let predicate =
            <@ Func<OwnerEntity, bool>(fun x -> x.Id = id) @>
            |> Lambda.toExpression

        let! entity = 
            ctx.Owners
                .Where(predicate)
                .FirstOrDefaultAsync()
         
        return Option.ofNull entity
    }
    

    let getOwnerByNameQuery (name: string) =
        let formatter (cmd: SQLiteCommand) =
            cmd.CommandText <- "SELECT * FROM Owner WHERE Name = @Name"
            cmd.Parameters.AddWithValue("@Name", name) |> ignore

        let operation (cmd: SQLiteCommand) = task {
            use! reader = cmd.ExecuteReaderAsync();
            
            let mutable records: OwnerDbRecord list = []
            while reader.Read() do
                records <- (mapDbRecord reader)::records
            
            return records
        }

        (formatter, operation)


    let getInsertOwnerCommand (x: OwnerDbRecord) =
        let formatter (cmd: SQLiteCommand) =
            let cmdText =
                """
                    insert into Owner(Id, Name)
                    values (@Id, @Name)
                """

            cmd.CommandText <- cmdText
            cmd.Parameters.AddWithValue("@Id", x.EntityData.Id.ToString()) |> ignore
            cmd.Parameters.AddWithValue("@Name", x.Model.Name) |> ignore

        let operation (cmd: SQLiteCommand) = task {
            return! cmd.ExecuteNonQueryAsync()
        }

        (formatter, operation)

