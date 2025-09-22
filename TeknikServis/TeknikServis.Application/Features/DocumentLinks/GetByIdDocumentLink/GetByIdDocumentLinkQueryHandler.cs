using AutoMapper;
using MediatR;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.DocumentLinks.GetByIdDocumentLink;

public sealed class GetByIdDocumentLinkQueryHandler(IDocumentLinkRepository documentLinkRepository, IMapper mapper) : IRequestHandler<GetByIdDocumentinkQuery, Result<DocumentLink>>
{
    private readonly IDocumentLinkRepository _documentLinkRepository = documentLinkRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<DocumentLink>> Handle(GetByIdDocumentinkQuery request, CancellationToken cancellationToken)
    {
        var documentLinkEntity = await _documentLinkRepository.GetByExpressionAsync(
            x => x.Id == request.Id,
            cancellationToken
        );

        if (documentLinkEntity is null)
            return Result<DocumentLink>.Failure("Dokument Link bulunamadı.");

        var document = _mapper.Map<DocumentLink>(documentLinkEntity);
        return Result<DocumentLink>.Succeed(document);
    }
}
