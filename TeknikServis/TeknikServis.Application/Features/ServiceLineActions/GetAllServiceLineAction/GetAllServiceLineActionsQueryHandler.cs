using MediatR;
using Microsoft.EntityFrameworkCore;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.ServiceLineActions.GetAllServiceLineAction;

internal sealed class GetAllServiceLineActionQueryHandler : IRequestHandler<GetAllServiceLineActionQuery, Result<List<ServiceLineAction>>>
{
    private readonly IServiceLineActionsRepository _serviceLineActionsRepository;

    public GetAllServiceLineActionQueryHandler(IServiceLineActionsRepository serviceLineActionsRepository)
    {
        _serviceLineActionsRepository = serviceLineActionsRepository;
    }

    public async Task<Result<List<ServiceLineAction>>> Handle(GetAllServiceLineActionQuery request, CancellationToken cancellationToken)
    {
        var slac = await _serviceLineActionsRepository
            .GetAll()
            .Where(x => !x.IsDeleted)
            .ToListAsync(cancellationToken);

        return Result<List<ServiceLineAction>>.Succeed(slac);
    }
}
