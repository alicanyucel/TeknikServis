using MediatR;
using TS.Result;

namespace TeknikServis.Application.Features.DocumentLinks.CreateDcumentLink;

public sealed record CreateDocumentLinkCommand(
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
