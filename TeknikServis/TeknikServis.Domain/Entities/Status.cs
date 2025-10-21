using TeknikServis.Domain.Abstractions;

namespace TeknikServis.Domain.Entities;

public class Status : Entity<Guid>
{
    public string Name { get; set; } = default!;

}
