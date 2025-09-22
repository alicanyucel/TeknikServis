using MediatR;
using TS.Result;

namespace TeknikServis.Application.Features.ServiceLineActions.CreateServiceLineActions;

public sealed record CreateServiceLineActionsCommand(
    Guid ServiceActionId,
    DateTime ActionDate,
    Guid PersonId,
    Guid ProductId,
    Guid CustomerId,
    string Description,
    Guid StatusId,
    TimeOnly UpdatedTime,
    string UpdatedBy,
    string CreatedBy,
    TimeOnly CratedTime,
    DateTime CreateadAt,
    DateTime? UpdatedAt,
    bool IsDeleted
) : IRequest<Result<string>>;
