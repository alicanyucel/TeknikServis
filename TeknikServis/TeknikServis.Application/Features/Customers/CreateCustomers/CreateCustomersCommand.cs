using MediatR;
using TeknikServis.Domain.Enums;
using TS.Result;

namespace TeknikServis.Application.Features.Customers.CreateCustomers;

public sealed record CreateCustomerCommand(
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
CustomerType CustomerType
) : IRequest<Result<string>>;
