using MediatR;
using TeknikServis.Domain.Enums;
using TS.Result;

namespace TeknikServis.Application.Features.Customers.UpdateCustomers;

public sealed record UpdateCustomerCommand(
Guid Id,
string Name,
string Surname,
string PhoneNumber,
string Email,
string Address,
string City,
string Country,
string ZipCode,
string District,
string Neighborhood,
CustomerType CustomerType,
TimeOnly UpdatedTime,
TimeOnly CreatedTime,
string? UpdatedBy,
string CreatedBy,
DateTime CreatedAt,
DateTime? UpdatedAt,
bool IsDeleted
) : IRequest<Result<string>>;
