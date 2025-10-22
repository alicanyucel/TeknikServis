using MediatR;
using TeknikServis.Application.Dtos;
using TS.Result;

namespace TeknikServis.Application.Features.Products.GetByIdProduct;

public sealed record GetProductByIdQuery(Guid Id) : IRequest<Result<ProductDto>>;