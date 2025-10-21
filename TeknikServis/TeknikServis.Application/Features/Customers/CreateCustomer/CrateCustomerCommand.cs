using MediatR;
using TeknikServis.Domain.ValueObjects;
using TS.Result;

namespace TeknikServis.Application.Features.Customers.CreateCustomer;

public sealed record CreateCustomerCommand(
    string Name,
    string TcNo,
    string VkNo,

    string Surname,
    string PhoneNumber,
    string Email,
    Address Address,
    int CustomerType,
    TimeOnly UpdatedTime,
    string UpdatedBy,
    string CreatedBy,
    TimeOnly CratedTime,
    DateTime CreateadAt,
    DateTime? UpdatedAt,
    bool IsDeleted
) : IRequest<Result<string>>;