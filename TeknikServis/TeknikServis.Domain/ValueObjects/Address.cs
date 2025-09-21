using Microsoft.EntityFrameworkCore;

namespace TeknikServis.Domain.ValueObjects;

[Owned]
public sealed record Address(string AddressLine, string City, string Neighborhood, string District, string ZipCode, string Country);
