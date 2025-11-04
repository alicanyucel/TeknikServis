using TeknikServis.Domain.Abstractions;
using TeknikServis.Domain.Enums;
using TeknikServis.Domain.ValueObjects;

namespace TeknikServis.Domain.Entities;

public sealed class Customer : Entity<Guid>
{
    public string Name { get; set; }= default!;
    public string? VkNo { get; set; }= default!;
    public string? TcNo { get; set; }= default!;
    public string Surname { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
    public Address Address { get; set; } = default!;
    public CustomerType CustomerType { get; set; } = CustomerType.Invidual;

    // New location references
    public int CountryId { get; set; }
    public Country Country { get; set; } = null!;
    public int ProvinceId { get; set; }
    public Province Province { get; set; } = null!;
    public int DistrictId { get; set; }
    public District District { get; set; } = null!;

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
