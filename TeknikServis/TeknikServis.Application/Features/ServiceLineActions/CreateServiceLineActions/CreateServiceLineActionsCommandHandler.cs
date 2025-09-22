using AutoMapper;
using GenericRepository;
using MediatR;
using TeknikServis.Application.Features.Customers.CreateCustomer;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.ServiceLineActions.CreateServiceLineActions;

internal sealed class CreateServiceLineActionsComamndHandler(IServiceLineActionsRepository servisLineActionsRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateServiceLineActionsCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateServiceLineActionsCommand request, CancellationToken cancellationToken)
    {
        ServiceLineAction  serviceLineAction = mapper.Map<ServiceLineAction>(request);
        await servisLineActionsRepository.AddAsync(serviceLineAction, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Servis Line Actions kaydı yapıldı";
    }
}