namespace TeknikServis.Application.Dtos;

public sealed record UserDto(
 Guid Id,
 string FirstName,
 string LastName,
 string Email,
 bool IsDeleted
);
