using TeknikServis.Domain.Enums;

namespace TeknikServis.Application.Dtos;

public sealed class CustomerDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
    public string District { get; set; } = null!;
    public string Neighborhood { get; set; } = null!;
    public CustomerType CustomerType { get; set; } = null!;
    public TimeOnly CreatedTime { get; set; }
    public TimeOnly UpdatedTime { get; set; }
    public string CreatedBy { get; set; } = null!;
    public string UpdatedBy { get; set; } = null!;
    public DateTime CreateadAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
}