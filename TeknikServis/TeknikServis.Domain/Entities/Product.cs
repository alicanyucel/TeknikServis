using TeknikServis.Domain.Abstractions;
using TeknikServis.Domain.Enums;

namespace TeknikServis.Domain.Entities;

public sealed class Product : Entity
{
    public string Brand { get; set; } = default!;
    public string Model { get; set; } = default!;
    public string SerialNumber { get; set; } = default!;
    public string Description { get; set; } = default!;
    public Guid CustomerId { get; set; }
    public required Customer Customer { get; set; }
    public required ProductType ProductType { get; set; } = ProductType.Accessory;
}
