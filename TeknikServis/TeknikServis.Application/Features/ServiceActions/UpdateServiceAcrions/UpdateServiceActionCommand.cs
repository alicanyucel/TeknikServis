using MediatR;
using TS.Result;

namespace TeknikServis.Application.Features.ServiceActions.UpdateServiceAcrions;

public sealed record UpdateServiceActionCommand(
Guid Id,
DateTime ActionDate,
string Description,
Guid PersonId,
Guid StatusId,
Guid CustomerId,
TimeOnly UpdatedTime,
string UpdatedBy,
string CreatedBy,
TimeOnly CratedTime,
DateTime CreateadAt,
DateTime? UpdatedAt,
bool IsDeleted
) : IRequest<Result<string>>;
