using MediatR;
using TS.Result;

namespace TeknikServis.Application.Features.Customers.DeleteCustomers;

public sealed record DeleteCustomerCommand(Guid Id) : IRequest<Result<string>>;
