using System.ComponentModel.DataAnnotations;

namespace TeknikServis.Domain.Abstractions;

public abstract class Entity<TKey>
{
    [Key]
    public TKey Id { get; set; } = default!;
    public required TimeOnly UpdatedTime { get; set; } = TimeOnly.FromDateTime(DateTime.Now);
    public required TimeOnly CreatedTime { get; set; } = TimeOnly.FromDateTime(DateTime.Now);
    public required string UpdatedBy { get; set; } = default!;
    public required string CreatedBy { get; set; } = default!;
    public required DateTime CreateadAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; } = false;
    protected Entity()
    {
        if (typeof(TKey) == typeof(Guid))
        {
            Id = (TKey)(object)Guid.NewGuid();
        }
    }
}

