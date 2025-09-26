public sealed class AppUser : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpires { get; set; }
    public required TimeOnly UpdatedTime { get; set; }
    public required TimeOnly CreatedTime { get; set; }
    public required string UpdatedBy { get; set; }
    public required string CreatedBy { get; set; }
    public required DateTime CreatedAt { get; set; }   
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
}
