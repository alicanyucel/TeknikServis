using MediatR;
using TS.Result;

namespace TeknikServis.Application.Features.Customers.CreateCustomer;

public sealed record CreateCustomerCommand(
 string Name,
 string Surname,
 string PhoneNumber,
 string Email,
 string AddressLine,
 string ZipCode,
 string Country,
 Guid NeighborhoodId,
 int CustomerValue
) : IRequest<Result<string>>;
