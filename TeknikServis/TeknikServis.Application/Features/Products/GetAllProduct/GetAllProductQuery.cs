using MediatR;
using TeknikServis.Application.Dtos;
using TS.Result;

namespace TeknikServis.Application.Features.Products.GetAllProduct;

public sealed record GetAllProductQuery : IRequest<Result<List<ProductDto>>>;