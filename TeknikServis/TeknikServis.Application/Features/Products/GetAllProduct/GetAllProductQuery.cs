using MediatR;
using TeknikServis.Domain.Entities;
using TS.Result;

namespace TeknikServis.Application.Features.Products.GetAllProduct;

public sealed record GetAllProductQuery : IRequest<Result<List<Product>>>;