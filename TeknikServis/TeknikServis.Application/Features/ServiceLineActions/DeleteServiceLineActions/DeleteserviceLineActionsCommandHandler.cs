using GenericRepository;
using MediatR;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.ServiceLineActions.DeleteServiceLineActions;

public sealed class DeleteServiceLineActionCommandHandler : IRequestHandler<DeleteServiceLineActionCommand, Result<string>>
{
    private readonly IServiceLineActionsRepository _serviceLineActionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteServiceLineActionCommandHandler(IServiceLineActionsRepository serviceLineActionRepository, IUnitOfWork unitOfWork)
    {
        _serviceLineActionRepository = serviceLineActionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteServiceLineActionCommand request, CancellationToken cancellationToken)
    {
        var slac = await _serviceLineActionRepository.GetByExpressionAsync(
            x => x.Id == request.Id,
            cancellationToken
        );

        if (slac== null)
            return Result<string>.Failure("Servisline actions bulunamadı.");
        _serviceLineActionRepository.Delete(slac);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return "ServiceLine Actions silindi";
    }
}