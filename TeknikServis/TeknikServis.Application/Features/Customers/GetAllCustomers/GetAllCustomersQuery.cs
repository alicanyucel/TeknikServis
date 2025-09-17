using MediatR;
using TeknikServis.Application.Dtos;
using TS.Result;

namespace TeknikServis.Application.Features.Customers.GetAllCustomers;

public sealed record GetAllCustomersQuery : IRequest<Result<List<CustomerDto>>>;
