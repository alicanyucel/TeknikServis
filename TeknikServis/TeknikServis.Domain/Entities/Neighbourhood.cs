using TeknikServis.Domain.Abstractions;

namespace TeknikServis.Domain.Entities;

public class Neighbourhood : Entity<int>
{
    public int Nr { get; set; }
    public int Code { get; set; }
    public string Name { get; set; } = default!;
    public int DistrictId { get; set; }
    public District District { get; set; } = default!;
}

