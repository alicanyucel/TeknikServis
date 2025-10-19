using Microsoft.AspNetCore.Identity;

namespace TeknikServis.Domain.Entities;

public sealed class AppUserRole : IdentityUserRole<Guid>
{
    public required AppUser User { get; set; }
    public required AppRole Role { get; set; }
}