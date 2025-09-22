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
            x => x.Id == request.Id,
            cancellationToken
        );

        if (status == null)
            return Result<string>.Failure("Durum bulunamadı.");
        _statusRepository.Delete(status);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return "Durum silindi";
    }
}
