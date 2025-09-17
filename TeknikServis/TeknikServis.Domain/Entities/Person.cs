using TeknikServis.Domain.Abstractions;
using TeknikServis.Domain.Enums;

namespace TeknikServis.Domain.Entities;

public sealed class Person : Entity
{
    public string Name { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string FullName => string.Join(" ", Name, LastName);
    public required ExpertiseArea ExpertiseArea { get; set; } = ExpertiseArea.Electronics;
    public required ICollection<ServiceAction> Actions { get; set; }
}
