using MediatR;
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
    int CustomerValue,
    TimeOnly UpdatedTime,
    TimeOnly CreatedTime,
    string? UpdatedBy,
    string CreatedBy,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    bool IsDeleted
) :IRequest<Result<string>>;

