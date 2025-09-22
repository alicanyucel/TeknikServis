using AutoMapper;
using GenericRepository;
using MediatR;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Statuses.UpdateStatus;

internal sealed class UpdateStatusCommandHandler(IStatusRepository statusRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateStatusCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateStatusCommand request, CancellationToken cancellationToken)
    {
        Status? status = await statusRepository.GetByExpressionWithTrackingAsync(P => P.Id == request.Id, cancellationToken);
        if (status == null)
        {
            return Result<string>.Failure("Durum bulunamadi.");
        }
        mapper.Map(request, status);
        statusRepository.Update(status);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Durum güncellendi.";

    }
}
