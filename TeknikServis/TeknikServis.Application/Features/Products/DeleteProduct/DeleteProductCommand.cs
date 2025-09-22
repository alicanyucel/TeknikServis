using MediatR;
using TS.Result;

namespace TeknikServis.Application.Features.Products.DeleteProduct;

public sealed record DeleteProductCommand(Guid Id) : IRequest<Result<string>>;