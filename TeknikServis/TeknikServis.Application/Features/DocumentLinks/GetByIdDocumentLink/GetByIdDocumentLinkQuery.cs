using MediatR;
using TeknikServis.Domain.Entities;
using TS.Result;

namespace TeknikServis.Application.Features.DocumentLinks.GetByIdDocumentLink;

public sealed record GetByIdDocumentinkQuery(Guid Id) : IRequest<Result<DocumentLink>>;
