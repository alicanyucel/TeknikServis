using TeknikServis.Domain.Abstractions;

namespace TeknikServis.Domain.Entities;

public class Status : Entity
{
    public string Name { get; set; } = default!;

}
