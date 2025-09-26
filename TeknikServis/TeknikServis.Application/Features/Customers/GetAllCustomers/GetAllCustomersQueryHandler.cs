using MediatR;
using Microsoft.EntityFrameworkCore;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Customers.GetAllCustomers;

internal sealed class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQuery, Result<List<Customer>>>
{
    private readonly ICustomerRepository _customerRepository;

    public GetAllCustomerQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Result<List<Customer>>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
    {
        var customers = await _customerRepository
            .GetAll()
            .Where(x => !x.IsDeleted)
            .ToListAsync(cancellationToken);

        return Result<List<Customer>>.Succeed(customers);
    }
}
