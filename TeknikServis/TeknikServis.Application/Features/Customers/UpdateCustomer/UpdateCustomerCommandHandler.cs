using AutoMapper;
using GenericRepository;
using MediatR;
using TeknikServis.Application.Features.Customers.UpdateCustomer;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

internal sealed class UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateCustomerCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        Customer? customer = await customerRepository.GetByExpressionWithTrackingAsync(P => P.Id == request.Id, cancellationToken);
        if (customer == null)
        {
            return Result<string>.Failure("musteri bulunamadi.");
        }
        mapper.Map(request, customer);
        customerRepository.Update(customer);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Müşteri güncellendi.";

    }
}