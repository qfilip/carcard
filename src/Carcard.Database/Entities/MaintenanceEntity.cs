namespace Carcard.Database.Entities;

public class MaintenanceEntity : BaseEntity
{
    public string? Repairman { get; set; }
    public DateTime Date { get; set; }
    public int Distance { get; set; }
    public string? Description { get; set; }
    public int Cost { get; set; }

    public Guid VehicleId { get; set; }
    public virtual VehicleEntity? Vehicle { get; set; }
}