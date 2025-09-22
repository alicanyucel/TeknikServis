using AutoMapper;
using GenericRepository;
using MediatR;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.ServiceLineActions.UpdateServiceLineActions;

internal sealed class UpdateServiceLineActionsCommandHandler(IServiceLineActionsRepository slacRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateServiceLineActionsCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateServiceLineActionsCommand request, CancellationToken cancellationToken)
    {
        ServiceLineAction? serviceLineAction = await slacRepository.GetByExpressionWithTrackingAsync(P => P.Id == request.Id, cancellationToken);
        if (serviceLineAction == null)
        {
            return Result<string>.Failure("Service Line Actions bulunamadi.");
        }
        mapper.Map(request, serviceLineAction);
        slacRepository.Update(serviceLineAction);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "ServiceLine Actions güncellendi.";

    }
}