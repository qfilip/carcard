namespace Carcard.Database.Entities;

public class OwnerEntity : BaseEntity
{
    public OwnerEntity()
    {
        Vehicles = new HashSet<VehicleEntity>();
    }

    public string? Name { get; set; }

    public virtual ICollection<VehicleEntity> Vehicles { get; set; }
}
