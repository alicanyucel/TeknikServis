using MediatR;
using TS.Result;

namespace TeknikServis.Application.Features.Products.UpdateProduct;

public sealed record UpdateProductCommand(
    Guid Id,
    string Brand,
    string Model,
    string SerialNumber,
    string Description,
    Guid CustomerId,
    int ProductType
) : IRequest<Result<string>>;
