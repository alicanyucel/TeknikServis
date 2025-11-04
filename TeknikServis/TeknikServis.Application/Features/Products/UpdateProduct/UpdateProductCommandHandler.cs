using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeknikServis.Domain.Enums;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Products.UpdateProduct;

internal sealed class UpdateProductCommandHandler(
    IProductRepository productRepository,
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateProductCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByExpressionWithTrackingAsync(P => P.Id == request.Id && !P.IsDeleted, cancellationToken);
        if (product is null)
        {
            return Result<string>.Failure("ürün bulunamadi.");
        }

      
        var customerExists = await customerRepository.GetAll().AnyAsync(c => c.Id == request.CustomerId && !c.IsDeleted, cancellationToken);
        if (!customerExists)
            return Result<string>.Failure("Geçersiz CustomerId. Müşteri bulunamadı.");

        
        product.Brand = request.Brand;
        product.Model = request.Model;
        product.SerialNumber = request.SerialNumber;
        product.Description = request.Description;
        product.CustomerId = request.CustomerId;
        product.ProductType = ProductType.FromValue(request.ProductType);

      
        product.UpdatedBy = product.UpdatedBy ?? "system";
        product.UpdatedAt = DateTime.UtcNow;
        product.UpdatedTime = TimeOnly.FromDateTime(DateTime.UtcNow);

        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Ürün güncellendi.";
    }
}