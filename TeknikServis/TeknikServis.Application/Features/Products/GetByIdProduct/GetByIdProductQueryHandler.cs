using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeknikServis.Application.Dtos;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Products.GetByIdProduct;

public sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var p = await _productRepository
            .GetAll()
            .Where(x => x.Id == request.Id && !x.IsDeleted)
            .Include(p => p.Customer)
            .Include(p => p.StatusHistory)
            .FirstOrDefaultAsync(cancellationToken);

        if (p is null)
            return Result<ProductDto>.Failure("Ürün bulunamadı veya silinmiş.");

        var dto = new ProductDto(
            Id: p.Id,
            Brand: p.Brand,
            Model: p.Model,
            SerialNumber: p.SerialNumber,
            Description: p.Description,
            CustomerId: p.CustomerId,
            CustomerName: p.Customer != null ? p.Customer.Name + " " + p.Customer.Surname : string.Empty,
            ProductTypeName: p.ProductType.Name,
            UpdatedAt: p.UpdatedAt,
            CreateadAt: p.CreateadAt,
            CreatedTime: p.CreatedTime,
            UpdatedTime: p.UpdatedTime,
            CreatedBy: p.CreatedBy,
            UpdatedBy: p.UpdatedBy,
            IsDeleted: p.IsDeleted
        );
        return Result<ProductDto>.Succeed(dto);
    }
}
