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
        var serviceActions = await _serviceActionRepository
            .GetAll()
            .Where(x => !x.IsDeleted)
            .Include(sa => sa.Person)
            .Include(sa => sa.Status)
            .Include(sa => sa.Customer)
            .Include(sa => sa.DocumentLinks)
            .Include(sa => sa.VideoLinks)
            .Include(sa => sa.ServiceLineActions)
                .ThenInclude(sla => sla.Product)
            .Include(sa => sa.ServiceLineActions)
                .ThenInclude(sla => sla.Person)
            .Include(sa => sa.ServiceLineActions)
                .ThenInclude(sla => sla.Customer)
            .Include(sa => sa.ServiceLineActions)
                .ThenInclude(sla => sla.Status)
            .ToListAsync(cancellationToken);

        return Result<List<ServiceAction>>.Succeed(serviceActions);
    }
}
