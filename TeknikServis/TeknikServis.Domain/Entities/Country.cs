using TeknikServis.Domain.Abstractions;

namespace TeknikServis.Domain.Entities;

public class Country : Entity<int>
{
  
    public string Name { get; set; } = default!;
    public ICollection<Province> Provinces { get; set; } = new List<Province>();
}

