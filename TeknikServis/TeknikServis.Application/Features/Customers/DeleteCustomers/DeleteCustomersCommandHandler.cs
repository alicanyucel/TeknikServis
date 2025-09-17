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
            x => x.Id == request.Id,
            cancellationToken
        );

        if (customer == null)
            return Result<string>.Failure("Müşteri bulunamadı.");
        _customerRepository.Delete(customer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return "Müşteri silindi";
    }
}
