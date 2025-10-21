using Microsoft.EntityFrameworkCore;

namespace TeknikServis.Domain.ValueObjects;

[Owned]
public sealed record Address(string AddressLine);
