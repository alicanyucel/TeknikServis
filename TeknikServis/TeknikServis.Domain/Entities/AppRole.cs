using Microsoft.AspNetCore.Identity;

namespace TeknikServis.Domain.Entities;

public sealed class AppRole : IdentityRole<Guid>
{
    public required ICollection<AppUserRole> UserRoles { get; set; }
    public required string RoleName { get; set; }
    public string Description { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
}
