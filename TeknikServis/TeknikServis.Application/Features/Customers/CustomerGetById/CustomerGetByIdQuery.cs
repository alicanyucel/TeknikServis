using MediatR;
using TeknikServis.Application.Dtos;
using TS.Result;

namespace TeknikServis.Application.Features.Customers.CustomerGetById;

public sealed record GetCustomerByIdQuery(Guid Id) : IRequest<Result<CustomerDto>>;
