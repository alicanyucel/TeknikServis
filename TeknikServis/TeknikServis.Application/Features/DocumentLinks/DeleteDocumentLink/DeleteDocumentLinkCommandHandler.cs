using GenericRepository;
using MediatR;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.DocumentLinks.DeleteDocumentLink;

public sealed class DeleteDocumentLinkCommandHandler : IRequestHandler<DeleteDocumentLinkCommand, Result<string>>
{
    private readonly IDocumentLinkRepository _documentLinkRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDocumentLinkCommandHandler(IDocumentLinkRepository documentLinkRepository, IUnitOfWork unitOfWork)
    {
        _documentLinkRepository = documentLinkRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteDocumentLinkCommand request, CancellationToken cancellationToken)
    {
        var documentLink = await _documentLinkRepository.GetByExpressionAsync(
            x => x.Id == request.Id && !x.IsDeleted,
            cancellationToken
        );

        if (documentLink is null)
            return Result<string>.Failure("Döküment link bulunamadı veya zaten silinmiş.");

        documentLink.IsDeleted = true;
        documentLink.UpdatedAt = DateTime.UtcNow;
        documentLink.UpdatedBy = "admin"; 

        _documentLinkRepository.Update(documentLink);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Döküment link başarıyla silindi (soft delete).");
    }
}
