using Carcard.Database.Enums;

namespace Carcard.Database.Entities;

public class BaseEntity
{
    public Guid Id { get; set; }
    public eEntityStatus EntityStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}
