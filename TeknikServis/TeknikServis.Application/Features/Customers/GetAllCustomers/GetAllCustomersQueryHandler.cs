using MediatR;
using Microsoft.EntityFrameworkCore;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Customers.GetAllCustomers;

public sealed class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, Result<List<Customer>>>
{
    private readonly ICustomerRepository _customerRepository;

    public GetAllCustomersQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Result<List<Customer>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await _customerRepository.GetAll()
            .ToListAsync(cancellationToken);
        return Result<List<Customer>>.Succeed(customers);
    }
}
