namespace TeknikServis.Application.Dtos;

public sealed record UserDto(
 Guid Id,
 string FirstName,
 string LastName,
 string Email,
 IList<string> Roles,
 TimeOnly UpdatedTime,
 string UpdatedBy,
 string CreatedBy,
 TimeOnly CratedTime,
 DateTime CreateadAt,
 DateTime? UpdatedAt,
 bool IsDeleted
);
