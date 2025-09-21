using AutoMapper;
using GenericRepository;
using MediatR;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.ServiceActions.CreateServiceActions;

internal sealed class CreateServiceActionComamndHandler(IServiceActionRepository serviceActionRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateServiceActionCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateServiceActionCommand request, CancellationToken cancellationToken)
    {
        ServiceAction serviceAction = mapper.Map<ServiceAction>(request);
        await serviceActionRepository.AddAsync(serviceAction, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Servis action kaydı yapıldı";
    }
}
