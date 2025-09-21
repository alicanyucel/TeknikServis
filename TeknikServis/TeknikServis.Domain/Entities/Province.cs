using TeknikServis.Domain.Abstractions;

namespace TeknikServis.Domain.Entities;

public class Province : Entity
{
    public string Name { get; set; } = string.Empty;
    public ICollection<District> Districts { get; set; } = new List<District>();
}

