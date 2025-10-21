namespace TeknikServis.Application.Dtos;

public sealed record StatusDto(
    Guid Id,
    string Name,
    TimeOnly UpdatedTime = default,
    string UpdatedBy = "",
    string CreatedBy = "",
    TimeOnly CratedTime = default,
    DateTime CreateadAt = default,
    DateTime? UpdatedAt = null,
    bool IsDeleted = false
);
