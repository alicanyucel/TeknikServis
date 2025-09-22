using MediatR;
using TeknikServis.Domain.Entities;
using TS.Result;

namespace TeknikServis.Application.Features.DocumentLinks.GetAllDocumentLink;


public sealed record GetAllDocumentLinkQuery : IRequest<Result<List<DocumentLink>>>;