using MediatR;
using TS.Result;

namespace TeknikServis.Application.Features.Statuses.CreateSratus;

public sealed record CreateStatusCommand(
    string Name,
    Guid ProductId,
    TimeOnly UpdatedTime,
    string UpdatedBy,
    string CreatedBy,
    TimeOnly CreatedTime,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    bool IsDeleted
) : IRequest<Result<string>>;
