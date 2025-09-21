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
    int CustomerType,
    TimeOnly UpdatedTime,
    string UpdatedBy,
    string CreatedBy,
    DateTime CreateadAt,
    TimeOnly CreatedTime,
    DateTime? UpdatedAt,
    bool IsDeleted
) : IRequest<Result<string>>;
