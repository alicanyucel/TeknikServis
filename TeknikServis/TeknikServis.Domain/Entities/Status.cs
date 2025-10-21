using TeknikServis.Domain.Abstractions;

namespace TeknikServis.Domain.Entities;

public class Status : Entity<Guid>
{
    public string Name { get; set; } = default!;
    public Guid? ProductId { get; set; }
    public Product? Product { get; set; }
}
        