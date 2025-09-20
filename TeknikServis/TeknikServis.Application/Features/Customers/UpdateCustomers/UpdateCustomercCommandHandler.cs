using GenericRepository;
using MediatR;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Customers.UpdateCustomers;

public sealed class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Result<string>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByExpressionAsync(
            c => c.Id == request.Id,
            cancellationToken
        );

        if (customer is null)
            return Result<string>.Failure("Müşteri bulunamadı.");

        customer.Name = request.Name;
        customer.Surname = request.Surname;
        customer.PhoneNumber = request.PhoneNumber;
        customer.Email = request.Email;
        customer.Address = request.Address;
        customer.City = request.City;
        customer.Country = request.Country;
        customer.ZipCode = request.ZipCode;
        customer.District = request.District;
        customer.Neighborhood = request.Neighborhood;
        customer.CustomerType = request.CustomerType;
        customer.UpdatedTime = request.UpdatedTime;
        customer.CreatedTime = request.CreatedTime;
        customer.CreatedBy = request.CreatedBy;
        customer.UpdatedBy = request.UpdatedBy ?? "Unknown";
        customer.UpdatedAt = request.UpdatedAt;
        customer.IsDeleted = request.IsDeleted;
        _customerRepository.Update(customer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return "Müşteri güncelllendi";
    }
}
