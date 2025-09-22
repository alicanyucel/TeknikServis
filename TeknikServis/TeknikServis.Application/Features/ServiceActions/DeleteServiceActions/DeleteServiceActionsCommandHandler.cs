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
        var serviceActions = await _serviceActionRepository.GetByExpressionAsync(
            x => x.Id == request.Id,
            cancellationToken
        );

        if (serviceActions == null)
            return Result<string>.Failure("Service action bulunamadı.");
        _serviceActionRepository.Delete(serviceActions);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return "Servis action silindi";
    }
}