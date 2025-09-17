using TeknikServis.Domain.Abstractions;
using TeknikServis.Domain.Enums;

namespace TeknikServis.Domain.Entities;

public sealed class Customer : Entity
{
    public string Name { get; set; } = default!;
    public string Surname { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string City { get; set; } = default!;
    public string Country { get; set; } = default!;
    public string ZipCode { get; set; } = default!;
    public string District { get; set; } = default!;
    public string Neighborhood { get; set; } = default!;
    public required CustomerType CustomerType { get; set; } = CustomerType.Invidual;
    public ICollection<Product> Products { get; set; } = new List<Product>();
}