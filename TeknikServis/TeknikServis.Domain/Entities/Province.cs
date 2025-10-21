using TeknikServis.Domain.Abstractions;

namespace TeknikServis.Domain.Entities;

public sealed class Province : Entity<int>
{
    public string Name { get; set; } = default!;
    public int Code { get; set; }
    public int Ref { get; set; }
    public int CountryId { get; set; }
    public Country Country { get; set; } = default!;
    public ICollection<District> Districts { get; set; } = new List<District>();
}

