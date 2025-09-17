using MediatR;
using TeknikServis.Domain.Entities;
using TS.Result;

namespace TeknikServis.Application.Features.Customers.CustomerGetById;

public sealed record GetCustomerByIdQuery(Guid Id) : IRequest<Result<Customer>>;
