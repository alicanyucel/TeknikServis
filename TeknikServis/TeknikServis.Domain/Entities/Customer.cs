using TeknikServis.Domain.Abstractions;
using TeknikServis.Domain.Enums;
using TeknikServis.Domain.ValueObjects;

namespace TeknikServis.Domain.Entities;

public sealed class Customer : Entity
{
    public string Name { get; set; }= default!;
    public string Surname { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
    public Address Address { get; set; } = default!;
    public CustomerType CustomerType { get; set; } = CustomerType.Invidual;
    public ICollection<Product> Products { get; set; } = new List<Product>();

}
