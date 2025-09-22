using MediatR;
using Microsoft.EntityFrameworkCore;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.DocumentLinks.GetAllDocumentLink;

internal sealed class GetAllDocumentLinkQueryHandler(IDocumentLinkRepository documentLinkRepository) : IRequestHandler<GetAllDocumentLinkQuery, Result<List<DocumentLink>>>
{
    public async Task<Result<List<DocumentLink>>> Handle(GetAllDocumentLinkQuery request, CancellationToken cancellationToken)
    {
        List<DocumentLink> documentLinks = await documentLinkRepository.GetAll().ToListAsync(cancellationToken);
        return documentLinks.ToList();
    }
}
