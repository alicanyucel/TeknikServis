using TeknikServis.Domain.Abstractions;

namespace TeknikServis.Domain.Entities;

public class District : Entity<int>
{
    public int Ref { get; set; }
    public int Code { get; set; }
    public string Name { get; set; } = default!;
    public string PostalCode { get; set; } = default!;
    public int ProvinceId { get; set; }
    public Province Province { get; set; } = default!;
    public ICollection<Neighbourhood> Neighbourhoods { get; set; } = new List<Neighbourhood>();
}

