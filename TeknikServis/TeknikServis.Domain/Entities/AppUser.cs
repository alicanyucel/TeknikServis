using Microsoft.AspNetCore.Identity;

namespace TeknikServis.Domain.Entities;

public sealed class AppUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => string.Join(" ", FirstName, LastName);
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpires { get; set; }
    public bool IsDeleted { get; set; } = false;
    public ICollection<AppUserRole> UserRoles { get; set; } = new List<AppUserRole>();
    public DateTime? CreateadAt { get; set; }
    public TimeOnly? CratedTime { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public TimeOnly? UpdatedTime { get; set; }
    public string? UpdatedBy { get; set; }
}
