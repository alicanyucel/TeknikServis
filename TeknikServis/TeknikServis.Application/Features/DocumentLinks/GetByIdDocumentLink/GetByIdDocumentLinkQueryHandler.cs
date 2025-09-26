using AutoMapper;
using MediatR;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.DocumentLinks.GetByIdDocumentLink;

public sealed class GetByIdDocumentLinkQueryHandler : IRequestHandler<GetByIdDocumentinkQuery, Result<DocumentLink>>
{
    private readonly IDocumentLinkRepository _documentLinkRepository;
    private readonly IMapper _mapper;

    public GetByIdDocumentLinkQueryHandler(IDocumentLinkRepository documentLinkRepository, IMapper mapper)
    {
        _documentLinkRepository = documentLinkRepository;
        _mapper = mapper;
    }

    public async Task<Result<DocumentLink>> Handle(GetByIdDocumentinkQuery request, CancellationToken cancellationToken)
    {
        var documentLinkEntity = await _documentLinkRepository.GetByExpressionAsync(
            x => x.Id == request.Id && !x.IsDeleted,
            cancellationToken
        );

        if (documentLinkEntity is null)
            return Result<DocumentLink>.Failure("Doküment link bulunamadı veya silinmiş.");

        var document = _mapper.Map<DocumentLink>(documentLinkEntity);
        return Result<DocumentLink>.Succeed(document);
    }
}
