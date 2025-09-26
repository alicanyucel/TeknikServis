using GenericRepository;
using MediatR;
using TeknikServis.Application.Features.Customers.DeleteCustomers;
using TeknikServis.Domain.Repositories;
using TS.Result;

public sealed class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Result<string>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByExpressionAsync(
            x => x.Id == request.Id && !x.IsDeleted,
            cancellationToken
        );

        if (customer is null)
            return Result<string>.Failure("Müşteri bulunamadı veya zaten silinmiş.");

        customer.IsDeleted = true;
        customer.UpdatedTime = TimeOnly.FromDateTime(DateTime.Now);
        customer.UpdatedAt = DateTime.UtcNow;
        customer.UpdatedBy = "admin"; 

        _customerRepository.Update(customer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Müşteri başarıyla silindi (soft delete).");
    }
}
