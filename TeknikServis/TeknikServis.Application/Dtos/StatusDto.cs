namespace TeknikServis.Application.Dtos;

public sealed record StatusDto(
    Guid Id,
    string Name,
    TimeOnly UpdatedTime,
    string UpdatedBy,
    string CreatedBy,
    TimeOnly CratedTime,
    DateTime CreateadAt,
    DateTime? UpdatedAt,
    bool IsDeleted
);
