namespace TeknikServis.Application.Dtos;

public sealed record UserDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    IList<string> Roles = null!,
    TimeOnly UpdatedTime = default,
    string UpdatedBy = "",
    string CreatedBy = "",
    TimeOnly CratedTime = default,
    DateTime CreateadAt = default,
    DateTime? UpdatedAt = null,
    bool IsDeleted = false
);
