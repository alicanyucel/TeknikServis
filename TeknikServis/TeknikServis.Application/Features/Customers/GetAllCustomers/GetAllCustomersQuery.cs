using MediatR;
using TeknikServis.Domain.Entities;
using TS.Result;

namespace TeknikServis.Application.Features.Customers.GetAllCustomers;

public sealed record GetAllCustomersQuery() : IRequest<Result<List<Customer>>>;
