using MediatR;
using Microsoft.EntityFrameworkCore;
using TeknikServis.Application.Dtos;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Products.GetAllProduct;

internal sealed class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, Result<List<ProductDto>>>
{
    private readonly IProductRepository _productRepository;

    public GetAllProductQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<List<ProductDto>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository
            .GetAll()
            .Where(x => !x.IsDeleted)
            .Include(p => p.Customer)
            .Include(p => p.StatusHistory)
            .Select(p => new ProductDto(
                p.Id,
                p.Brand,
                p.Model,
                p.SerialNumber,
                p.Description,
                p.CustomerId,
                p.Customer != null ? p.Customer.Name + " " + p.Customer.Surname : string.Empty,
                p.ProductType.Name,
                p.UpdatedAt,
                p.CreateadAt,
                p.CreatedTime,
                p.UpdatedTime,
                p.CreatedBy,
                p.UpdatedBy,
                p.IsDeleted
            ))
            .ToListAsync(cancellationToken);

        return Result<List<ProductDto>>.Succeed(products);
    }
}
