using MediatR;
using TeknikServis.Domain.ValueObjects;
using TS.Result;

namespace TeknikServis.Application.Features.Customers.UpdateCustomer;

public sealed record UpdateCustomerCommand(
 Guid Id,
 string Name,
 string Surname,
 string PhoneNumber,
 string Email,
 Address Address,
 int CustomerType
) : IRequest<Result<string>>;

