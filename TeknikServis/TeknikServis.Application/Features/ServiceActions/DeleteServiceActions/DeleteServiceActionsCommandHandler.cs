using GenericRepository;
using MediatR;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.ServiceActions.DeleteServiceActions;

public sealed class DeleteServiceActionCommandHandler : IRequestHandler<DeleteServiceActionsCommand, Result<string>>
{
    private readonly IServiceActionRepository _serviceActionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteServiceActionCommandHandler(IServiceActionRepository serviceActionRepository, IUnitOfWork unitOfWork)
    {
        _serviceActionRepository = serviceActionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteServiceActionsCommand request, CancellationToken cancellationToken)
    {
        var serviceAction = await _serviceActionRepository.GetByExpressionAsync(
            x => x.Id == request.Id && !x.IsDeleted,
            cancellationToken
        );

        if (serviceAction is null)
            return Result<string>.Failure("Servis action bulunamadı veya zaten silinmiş.");

        serviceAction.IsDeleted = true;
        serviceAction.UpdatedAt = DateTime.UtcNow;
        serviceAction.UpdatedBy = "admin"; 

        _serviceActionRepository.Update(serviceAction);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Servis action başarıyla silindi (soft delete).");
    }
}
