using GenericRepository;
using MediatR;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Statuses.DeleteStatus;

public sealed class DeleteStatusCommandHandler : IRequestHandler<DeleteStatusCommand, Result<string>>
{
    private readonly IStatusRepository _statusRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteStatusCommandHandler(IStatusRepository statusRepository, IUnitOfWork unitOfWork)
    {
        _statusRepository = statusRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteStatusCommand request, CancellationToken cancellationToken)
    {
        var status = await _statusRepository.GetByExpressionAsync(
            x => x.Id == request.Id && !x.IsDeleted,
            cancellationToken
        );

        if (status is null)
            return Result<string>.Failure("Durum bulunamadı veya zaten silinmiş.");

        status.IsDeleted = true;
        status.UpdatedAt = DateTime.UtcNow;
        status.UpdatedBy = "admin"; 

        _statusRepository.Update(status);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Durum başarıyla silindi (soft delete).");
    }
}
