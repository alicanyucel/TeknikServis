using MediatR;
using TS.Result;

namespace TeknikServis.Application.Features.DocumentLinks.CreateDcumentLink;

public sealed record CreateDocumentLinkCommand(
string Url,
string Description,
Guid ServiceActionId
) : IRequest<Result<string>>;
