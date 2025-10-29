using TeknikServis.Domain.Abstractions;

namespace TeknikServis.Domain.Entities;

public sealed class Country : Entity<int>
{
    public string Name { get; set; } = null!;
    public string? Code { get; set; }
    public ICollection<Province> Provinces { get; } = new List<Province>();
}