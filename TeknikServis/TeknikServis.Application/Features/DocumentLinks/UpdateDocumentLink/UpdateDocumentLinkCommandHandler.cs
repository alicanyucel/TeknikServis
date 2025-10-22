using GenericRepository;
using MediatR;
using System.Text;
using System.Text.RegularExpressions;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.DocumentLinks.UpdateDocumentLink;

internal sealed class UpdateDocumentLinkCommandHandler(IDocumentLinkRepository documentLinkRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateDocumentLinkCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateDocumentLinkCommand request, CancellationToken cancellationToken)
    {
        DocumentLink? documentLink = await documentLinkRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.Id, cancellationToken);
        if (documentLink == null)
        {
            return Result<string>.Failure("documentLink bulunamadi.");
        }

        if (request.File is not null && request.File.Length > 0)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            Directory.CreateDirectory(uploadsFolder);
            var safeFileName = MakeSafeFileName(request.File.FileName);
            var uniqueFileName = $"{Guid.NewGuid()}_{safeFileName}";
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

    private static string MakeSafeFileName(string originalName)
    {
        var fileNameOnly = Path.GetFileNameWithoutExtension(originalName);
        var ext = Path.GetExtension(originalName);
        var normalized = fileNameOnly.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();
        foreach (var ch in normalized)
        {
            var uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(ch);
            if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                sb.Append(ch);
        }
        var withoutDiacritics = sb.ToString().Normalize(NormalizationForm.FormC);
        var safe = Regex.Replace(withoutDiacritics, @"\s+", "-");
        safe = Regex.Replace(safe, @"[^A-Za-z0-9_\-\.]", "");
        var safeExt = string.IsNullOrWhiteSpace(ext) ? ".bin" : ext;
        return $"{safe}{safeExt}";
    }
}
