using AutoMapper;
using GenericRepository;
using MediatR;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.ServiceActions.UpdateServiceAcrions;

internal sealed class UpdateServiceActionCommandHandler(IServiceActionRepository serviceActionRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateServiceActionCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateServiceActionCommand request, CancellationToken cancellationToken)
    {
        ServiceAction? serviceAction = await serviceActionRepository.GetByExpressionWithTrackingAsync(P => P.Id == request.Id, cancellationToken);
        if (serviceAction == null)
        {
            return Result<string>.Failure("servis action bulunamadi.");
        }
        mapper.Map(request, serviceAction);
        serviceActionRepository.Update(serviceAction);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Servis Action güncellendi.";

    }
}