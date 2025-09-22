using AutoMapper;
using MediatR;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Products.GetByIdProduct;

public sealed class GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper) : IRequestHandler<GetProductByIdQuery, Result<Product>>
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<Product>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var productEntity = await _productRepository.GetByExpressionAsync(
            x => x.Id == request.Id,
            cancellationToken
        );

        if (productEntity is null)
        return Result<Product>.Failure("Ürün bulunamadı.");
        var product = _mapper.Map<Product>(productEntity);
        return Result<Product>.Succeed(product);
    }
}