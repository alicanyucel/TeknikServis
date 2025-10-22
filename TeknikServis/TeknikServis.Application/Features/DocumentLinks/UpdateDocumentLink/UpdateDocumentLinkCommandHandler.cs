using AutoMapper;
using GenericRepository;
using MediatR;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.DocumentLinks.UpdateDocumentLink;

internal sealed class UpdateDocumentLinkCommandHandler(IDocumentLinkRepository documentLinkRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateDocumentLinkCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateDocumentLinkCommand request, CancellationToken cancellationToken)
    {
        DocumentLink? documentLink = await documentLinkRepository.GetByExpressionWithTrackingAsync(P => P.Id == request.Id, cancellationToken);
        if (documentLink == null)
        {
            return Result<string>.Failure("documentLink bulunamadi.");
        }
        if (request.File is not null && request.File.Length > 0)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            Directory.CreateDirectory(uploadsFolder);
            var uniqueFileName = $"{Guid.NewGuid()}_{request.File.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await request.File.CopyToAsync(stream, cancellationToken);
            documentLink.Url = $"/uploads/{uniqueFileName}";
        }
        documentLink.Description = request.Description;
        documentLink.ServiceActionId = request.ServiceActionId;
        documentLink.UpdatedTime = request.UpdatedTime;
        documentLink.UpdatedBy = request.UpdatedBy;
        documentLink.UpdatedAt = request.UpdatedAt;
        documentLink.IsDeleted = request.IsDeleted;
        documentLinkRepository.Update(documentLink);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Dokument Link güncellendi.";

    }
}
