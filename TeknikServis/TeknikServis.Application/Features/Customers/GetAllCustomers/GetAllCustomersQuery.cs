using MediatR;
using TeknikServis.Application.Dtos;
using TeknikServis.Domain.Entities;
using TS.Result;

namespace TeknikServis.Application.Features.Customers.GetAllCustomers;

public sealed record GetAllCustomerQuery : IRequest<Result<List<Customer>>>;
