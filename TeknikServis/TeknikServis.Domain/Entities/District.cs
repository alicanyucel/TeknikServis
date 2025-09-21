using TeknikServis.Domain.Abstractions;

namespace TeknikServis.Domain.Entities;

public class District : Entity
{
    public string Name { get; set; } = string.Empty;

    public Guid ProvinceId { get; set; }
    public Province Province { get; set; } = null!;

    public ICollection<Neighborhood> Neighborhoods { get; set; } = new List<Neighborhood>();
}
