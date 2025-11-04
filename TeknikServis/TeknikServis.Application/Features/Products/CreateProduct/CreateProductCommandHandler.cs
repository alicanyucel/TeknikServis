using AutoMapper;
using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Products.CreateProduct;

internal sealed class CreateProductComamndHandler(
    IProductRepository productRepository,
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<CreateProductCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var customerExists = await customerRepository.GetAll()
            .AnyAsync(c => c.Id == request.CustomerId && !c.IsDeleted, cancellationToken);
        if (!customerExists)
        {
            return Result<string>.Failure("Geçersiz CustomerId. Müşteri bulunamadı.");
        }

        Product product = mapper.Map<Product>(request);
        product.CreatedBy = product.CreatedBy ?? "system";
        product.UpdatedBy = product.UpdatedBy ?? "system";
        if (product.CreateadAt == default) product.CreateadAt = DateTime.UtcNow;
        if (product.CreatedTime == default) product.CreatedTime = TimeOnly.FromDateTime(DateTime.UtcNow);
        if (product.UpdatedTime == default) product.UpdatedTime = TimeOnly.FromDateTime(DateTime.UtcNow);

        await productRepository.AddAsync(product, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Ürün kaydı yapıldı";
    }
}
