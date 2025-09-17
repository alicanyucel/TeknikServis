using GenericRepository;
using MediatR;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Customers.CreateCustomers;

public sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result<string>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var emailExists = await _customerRepository.AnyAsync(
            x => x.Email == request.Email,
            cancellationToken
        );

        if (emailExists)
        {
            return Result<string>.Failure("Email zaten kayıtlı");
        }

        var customer = new Customer
        {
            Name = request.Name,
            Surname = request.Surname,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            Address = request.Address,
            City = request.City,
            Country = request.Country,
            ZipCode = request.ZipCode,
            District = request.District,
            Neighborhood = request.Neighborhood,
            CustomerType = request.CustomerType,
            UpdatedTime = TimeOnly.FromDateTime(DateTime.UtcNow),
            CreatedTime = TimeOnly.FromDateTime(DateTime.UtcNow),
            UpdatedBy = "System",
            CreatedBy = "System",
            CreateadAt = DateTime.Now
        };
        await _customerRepository.AddAsync(customer, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("müşteri eklendi.");

    }

}