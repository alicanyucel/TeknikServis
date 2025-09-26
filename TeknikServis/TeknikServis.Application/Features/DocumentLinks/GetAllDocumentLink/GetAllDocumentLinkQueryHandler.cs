using MediatR;
using Microsoft.EntityFrameworkCore;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.DocumentLinks.GetAllDocumentLink;

internal sealed class GetAllDocumentLinkQueryHandler : IRequestHandler<GetAllDocumentLinkQuery, Result<List<DocumentLink>>>
{
    private readonly IDocumentLinkRepository _documentLinkRepository;

    public GetAllDocumentLinkQueryHandler(IDocumentLinkRepository documentLinkRepository)
    {
        _documentLinkRepository = documentLinkRepository;
    }

    public async Task<Result<List<DocumentLink>>> Handle(GetAllDocumentLinkQuery request, CancellationToken cancellationToken)
    {
        var documentLinks = await _documentLinkRepository
            .GetAll()
            .Where(x => !x.IsDeleted)
            .ToListAsync(cancellationToken);

        return Result<List<DocumentLink>>.Succeed(documentLinks);
    }
}
