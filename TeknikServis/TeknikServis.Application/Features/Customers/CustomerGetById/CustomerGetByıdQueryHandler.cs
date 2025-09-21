using AutoMapper;
using MediatR;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Customers.CustomerGetById;

public sealed class GetCustomerByIdQueryHandler(ICustomerRepository customerRepository, IMapper mapper) : IRequestHandler<GetCustomerByIdQuery, Result<Customer>>
{
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<Customer>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customerEntity = await _customerRepository.GetByExpressionAsync(
            x => x.Id == request.Id,
            cancellationToken
        );

        if (customerEntity is null)
            return Result<Customer>.Failure("Müşteri bulunamadı.");

        var customer = _mapper.Map<Customer>(customerEntity);
        return Result<Customer>.Succeed(customer);
    }
}
