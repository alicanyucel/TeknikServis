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
int ProductType,
TimeOnly UpdatedTime,
string UpdatedBy,
string CreatedBy,
TimeOnly CratedTime,
DateTime CreateadAt,
DateTime? UpdatedAt,
bool IsDeleted
) : IRequest<Result<string>>;
