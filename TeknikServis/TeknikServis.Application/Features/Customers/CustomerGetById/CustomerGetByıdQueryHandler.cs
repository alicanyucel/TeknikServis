using MediatR;
using Microsoft.EntityFrameworkCore;
using TeknikServis.Application.Features.Customers.CustomerGetById;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

public sealed class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Result<Customer>>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Result<Customer>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetAll()
            .Include(x => x.Country)
            .Include(x => x.Province)
            .Include(x => x.District)
            .FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted, cancellationToken);

        if (customer is null)
            return Result<Customer>.Failure("Müşteri bulunamadı veya silinmiş.");

        return Result<Customer>.Succeed(customer);
    }
}
