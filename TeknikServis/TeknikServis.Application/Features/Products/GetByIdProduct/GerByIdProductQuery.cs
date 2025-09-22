using MediatR;
using TeknikServis.Domain.Entities;
using TS.Result;

namespace TeknikServis.Application.Features.Products.GetByIdProduct;

public sealed record GetProductByIdQuery(Guid Id) : IRequest<Result<Product>>;