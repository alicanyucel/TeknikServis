using MediatR;
using Microsoft.EntityFrameworkCore;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.ServiceLineActions.GetAllServiceLineAction;

internal sealed class GetAllServiceLineActionQueryHandler(IServiceLineActionsRepository serviceLineActionsRepository) : IRequestHandler<GetAllServiceLineActionQuery, Result<List<ServiceLineAction>>>
{
    public async Task<Result<List<ServiceLineAction>>> Handle(GetAllServiceLineActionQuery request, CancellationToken cancellationToken)
    {
        List<ServiceLineAction> slac = await serviceLineActionsRepository.GetAll().ToListAsync(cancellationToken);
        return slac.ToList();
    }
}