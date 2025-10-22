using AutoMapper;
using GenericRepository;
using MediatR;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.DocumentLinks.CreateDcumentLink;
internal sealed class CreateDocumentLinkComamndHandler(
    IDocumentLinkRepository documentRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<CreateDocumentLinkCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateDocumentLinkCommand request, CancellationToken cancellationToken)
    {
        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        Directory.CreateDirectory(uploadsFolder);
        var uniqueFileName = $"{Guid.NewGuid()}_{request.File.FileName}";
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);
        using var stream = new FileStream(filePath, FileMode.Create);
        await request.File.CopyToAsync(stream, cancellationToken);
        var fileUrl = $"/uploads/{uniqueFileName}";
        var document = mapper.Map<DocumentLink>(request);
        document.Url = fileUrl;
        await documentRepository.AddAsync(document, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Döküman kaydı yapıldı";
    }
}

