using MediatR;
using TS.Result;

namespace TeknikServis.Application.Features.Products.CreateProduct;

public sealed record CreateProductCommand(
 string Brand,
 string Model,
 string SerialNumber,
 string Description,
 Guid CustomerId,
 int ProductType
) : IRequest<Result<string>>;
