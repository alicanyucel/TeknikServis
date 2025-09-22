using MediatR;
using TS.Result;

namespace TeknikServis.Application.Features.DocumentLinks.DeleteDocumentLink;

public sealed record DeleteDocumentLinkCommand(Guid Id) : IRequest<Result<string>>;
