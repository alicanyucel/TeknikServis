using GenericRepository;
using MediatR;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Products.DeleteProduct;

public sealed class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<string>>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository=productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByExpressionAsync(
            x => x.Id == request.Id,
            cancellationToken
        );

        if (product == null)
            return Result<string>.Failure("Ürün bulunamadı.");
        _productRepository.Delete(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return "Ürün silindi";
    }
}