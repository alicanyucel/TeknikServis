namespace TeknikServis.Domain.Abstractions;
public abstract class Entity
{
    public required TimeOnly UpdatedTime { get; set; } = TimeOnly.FromDateTime(DateTime.Now);
    public required TimeOnly CreatedTime { get; set; } = TimeOnly.FromDateTime(DateTime.Now);
    public required string UpdatedBy { get; set; } = default!;
    public required string CreatedBy { get; set; } = default!;
    public required DateTime CreateadAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; } = false;
    public Guid Id { get; set; }
    protected Entity()
    {
        Id = Guid.NewGuid();
    }
}

