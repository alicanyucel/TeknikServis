using AutoMapper;
using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.ServiceLineActions.CreateServiceLineActions;

internal sealed class CreateServiceLineActionsComamndHandler(
    IServiceLineActionsRepository servisLineActionsRepository,
    IServiceActionRepository serviceActionRepository,
    IProductRepository productRepository,
    IPersonRepository personRepository,
    ICustomerRepository customerRepository,
    IStatusRepository statusRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper
) : IRequestHandler<CreateServiceLineActionsCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateServiceLineActionsCommand request, CancellationToken cancellationToken)
    {
        // Validate FK references before insert to avoid SQL FK exceptions
        var serviceActionExists = await serviceActionRepository.GetAll()
            .AnyAsync(sa => sa.Id == request.ServiceActionId && !sa.IsDeleted, cancellationToken);
        if (!serviceActionExists)
            return Result<string>.Failure("Geçersiz ServiceActionId. Kayıt bulunamadı.");

        var product = await productRepository.GetByExpressionAsync(p => p.Id == request.ProductId && !p.IsDeleted, cancellationToken);
        if (product is null)
            return Result<string>.Failure("Geçersiz ProductId. Ürün bulunamadı.");

        var personExists = await personRepository.GetAll()
            .AnyAsync(p => p.Id == request.PersonId && !p.IsDeleted, cancellationToken);
        if (!personExists)
            return Result<string>.Failure("Geçersiz PersonId. Personel bulunamadı.");

        var customerExists = await customerRepository.GetAll()
            .AnyAsync(c => c.Id == request.CustomerId && !c.IsDeleted, cancellationToken);
        if (!customerExists)
            return Result<string>.Failure("Geçersiz CustomerId. Müşteri bulunamadı.");

        var statusExists = await statusRepository.GetAll()
            .AnyAsync(s => s.Id == request.StatusId && !s.IsDeleted, cancellationToken);
        if (!statusExists)
            return Result<string>.Failure("Geçersiz StatusId. Durum bulunamadı.");

        // Optional consistency: ensure product belongs to the same customer
        if (product.CustomerId != request.CustomerId)
            return Result<string>.Failure("Ürün ile müşteri bilgisi uyuşmuyor.");

        ServiceLineAction serviceLineAction = mapper.Map<ServiceLineAction>(request);
        await servisLineActionsRepository.AddAsync(serviceLineAction, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Servis Line Actions kaydı yapıldı";
    }
}