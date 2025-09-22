using AutoMapper;
using GenericRepository;
using MediatR;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Products.UpdateProduct;

internal sealed class UpdateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateProductCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Product? product = await productRepository.GetByExpressionWithTrackingAsync(P => P.Id == request.Id, cancellationToken);
        if (product == null)
        {
            return Result<string>.Failure("ürün bulunamadi.");
        }
        mapper.Map(request, product);
        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Ürün güncellendi.";

    }
}