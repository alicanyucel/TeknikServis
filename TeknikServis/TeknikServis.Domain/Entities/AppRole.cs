using Microsoft.AspNetCore.Identity;

namespace TeknikServis.Domain.Entities;

public sealed class AppRole : IdentityRole<Guid>
{
    public ICollection<AppUserRole> UserRoles { get; set; } = new List<AppUserRole>();

}
