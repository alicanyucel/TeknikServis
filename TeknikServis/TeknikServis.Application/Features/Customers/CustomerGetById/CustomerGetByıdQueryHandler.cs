using AutoMapper;
using MediatR;
using TeknikServis.Application.Features.Customers.CustomerGetById;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

public sealed class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Result<Customer>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<Result<Customer>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customerEntity = await _customerRepository.GetByExpressionAsync(
            x => x.Id == request.Id && !x.IsDeleted,
            cancellationToken
        );

        if (customerEntity is null)
            return Result<Customer>.Failure("Müşteri bulunamadı veya silinmiş.");

        var customer = _mapper.Map<Customer>(customerEntity);
        return Result<Customer>.Succeed(customer);
    }
}
