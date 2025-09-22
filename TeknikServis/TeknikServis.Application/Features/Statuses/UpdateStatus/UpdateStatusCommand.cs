using MediatR;
using TS.Result;

namespace TeknikServis.Application.Features.Statuses.UpdateStatus;
public sealed record UpdateStatusCommand(
    Guid Id,
    string Name,
    TimeOnly UpdatedTime,
    string UpdatedBy,
    string CreatedBy,
    TimeOnly CratedTime,
    DateTime CreateadAt,
    DateTime? UpdatedAt,
    bool IsDeleted
) : IRequest<Result<string>>;
