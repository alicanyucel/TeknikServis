using TeknikServis.Domain.Abstractions;
using TeknikServis.Domain.Enums;

namespace TeknikServis.Domain.Entities;

public sealed class Customer : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string AddressLine { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

    public Guid NeighborhoodId { get; set; }
    public Neighborhood Neighborhood { get; set; } = null!;

    public CustomerType CustomerType { get; set; } = CustomerType.Invidual;
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
