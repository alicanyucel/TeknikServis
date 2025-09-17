using GenericRepository;
using MediatR;
using TeknikServis.Domain.Entities;
using TS.Result;

namespace TeknikServis.Application.Features.Customers.CustomerGetById;

public sealed class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Result<Customer>>
{
    private readonly IRepository<Customer> _customerRepository;

    public GetCustomerByIdQueryHandler(IRepository<Customer> customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Result<Customer>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByExpressionAsync(
            x => x.Id == request.Id,
            cancellationToken
        );

        if (customer is null)
            return Result<Customer>.Failure("Müşteri bulunamadı.");

        return Result<Customer>.Succeed(customer);
    }
}
