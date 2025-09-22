using MediatR;
using TS.Result;

namespace TeknikServis.Application.Features.ServiceLineActions.UpdateServiceLineActions;

public sealed record UpdateServiceLineActionsCommand(
 Guid Id,
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
