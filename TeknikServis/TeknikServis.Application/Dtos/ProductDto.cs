namespace TeknikServis.Application.Dtos;

public sealed record ProductDto(
    Guid Id,
    string Brand,
    string Model,
    string SerialNumber,
    string Description,
    Guid CustomerId,
    string CustomerName,
    string ProductTypeName,
    DateTime? UpdatedAt,
    DateTime CreateadAt,
    TimeOnly CreatedTime,
    TimeOnly UpdatedTime,
    string CreatedBy,
    string UpdatedBy,
    bool IsDeleted
);
