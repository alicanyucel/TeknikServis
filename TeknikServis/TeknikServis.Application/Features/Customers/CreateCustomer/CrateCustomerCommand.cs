using MediatR;
using TeknikServis.Domain.ValueObjects;
using TS.Result;

namespace TeknikServis.Application.Features.Customers.CreateCustomer;

public sealed record CreateCustomerCommand(
    string Name,
    string Surname,
    string PhoneNumber,
    string Email,
    Address Address,
    int CustomerType
) : IRequest<Result<string>>;