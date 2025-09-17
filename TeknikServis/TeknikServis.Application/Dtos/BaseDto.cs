namespace TeknikServis.Application.Dtos;

public abstract record BaseDto(
    TimeOnly UpdatedTime,
    TimeOnly CreatedTime,
    string UpdatedBy,
    string CreatedBy,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    bool IsDeleted
);