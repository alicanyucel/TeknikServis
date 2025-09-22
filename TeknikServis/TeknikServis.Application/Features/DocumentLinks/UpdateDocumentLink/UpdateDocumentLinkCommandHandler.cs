using AutoMapper;
using GenericRepository;
using MediatR;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.DocumentLinks.UpdateDocumentLink;

internal sealed class UpdateDocumentLinkCommandHandler(IDocumentLinkRepository documentLinkRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateDocumentLinkCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateDocumentLinkCommand request, CancellationToken cancellationToken)
    {
        DocumentLink? documentLink = await documentLinkRepository.GetByExpressionWithTrackingAsync(P => P.Id == request.Id, cancellationToken);
        if (documentLink == null)
        {
            return Result<string>.Failure("documentLink bulunamadi.");
        }
        mapper.Map(request, documentLink);
        documentLinkRepository.Update(documentLink);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Dokument Link güncellendi.";

    }
}
