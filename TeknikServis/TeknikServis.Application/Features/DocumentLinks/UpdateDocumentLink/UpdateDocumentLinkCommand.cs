using MediatR;
using TS.Result;

namespace TeknikServis.Application.Features.DocumentLinks.UpdateDocumentLink;

public sealed record UpdateDocumentLinkCommand(
Guid Id,
string Url,
string Description,
Guid ServiceActionId,
TimeOnly UpdatedTime,
string UpdatedBy,
string CreatedBy,
TimeOnly CratedTime,
DateTime CreateadAt,
DateTime? UpdatedAt,
bool IsDeleted
) : IRequest<Result<string>>;
