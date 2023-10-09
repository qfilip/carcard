namespace Carcard.Database.Entities;

public class VehicleEntity : BaseEntity
{
    public VehicleEntity()
    {
        Maintenances = new HashSet<MaintenanceEntity>();
    }

    public string? Model { get; set;  }
    public string? Vendor { get; set; }
    public int Year { get; set;  }
    
    public Guid OwnerId { get; set; }
    public virtual OwnerEntity? Owner { get; set;  }

    public virtual ICollection<MaintenanceEntity> Maintenances { get; set; }
}
