using MediatR;
using Microsoft.EntityFrameworkCore;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.ServiceActions.GetAllServiceActions;

internal sealed class GetAllServiceActionQueryHandler : IRequestHandler<GetAllServiceActionQuery, Result<List<ServiceAction>>>
{
    private readonly IServiceActionRepository _serviceActionRepository;

    public GetAllServiceActionQueryHandler(IServiceActionRepository serviceActionRepository)
    {
        _serviceActionRepository = serviceActionRepository;
    }

    public async Task<Result<List<ServiceAction>>> Handle(GetAllServiceActionQuery request, CancellationToken cancellationToken)
    {
        List<ServiceAction> serviceActions = await _serviceActionRepository.GetAll().ToListAsync(cancellationToken);
        return Result<List<ServiceAction>>.Succeed(serviceActions);
    }
}
