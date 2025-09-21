using AutoMapper;
using GenericRepository;
using MediatR;
using TeknikServis.Application.Features.Customers.CreateCustomer;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.DocumentLinks.CreateDcumentLink;

internal sealed class CreateDocumentLinkComamndHandler(IDocumentLinkRepository documentRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateDocumentLinkCommand, Result<string>>
{
   
    public async Task<Result<string>> Handle(CreateDocumentLinkCommand request, CancellationToken cancellationToken)
    {
        DocumentLink document = mapper.Map<DocumentLink>(request);
        await documentRepository.AddAsync(document, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Döküman kaydı yapıldı";
    }
}