using AutoMapper;
using MediatR;
using TeknikServis.Application.Dtos;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Customers.CustomerGetById;

public sealed class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Result<CustomerDto>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<Result<CustomerDto>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByExpressionAsync(
            x => x.Id == request.Id,
            cancellationToken
        );

        if (customer is null)
            return Result<CustomerDto>.Failure("Müşteri bulunamadı.");

        var customerDto = _mapper.Map<CustomerDto>(customer);
        return Result<CustomerDto>.Succeed(customerDto);
    }
}
