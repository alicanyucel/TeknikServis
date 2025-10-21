using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.ServiceActions.GetByIdServiceActions;

public sealed class GetServiceActionByIdQueryHandler : IRequestHandler<GetServiceActionByIdQuery, Result<ServiceAction>>
{
    private readonly IServiceActionRepository _serviceActionRepository;
    private readonly IMapper _mapper;

    public GetServiceActionByIdQueryHandler(IServiceActionRepository serviceActionRepository, IMapper mapper)
    {
        _serviceActionRepository = serviceActionRepository;
        _mapper = mapper;
    }

    public async Task<Result<ServiceAction>> Handle(GetServiceActionByIdQuery request, CancellationToken cancellationToken)
    {
        var sa = await _serviceActionRepository
            .GetAll()
            .Where(x => x.Id == request.Id && !x.IsDeleted)
            .Include(x => x.Person)
            .Include(x => x.Status)
            .Include(x => x.Customer)
            .Include(x => x.DocumentLinks)
            .Include(x => x.VideoLinks)
            .Include(x => x.ServiceLineActions)
                .ThenInclude(sla => sla.Product)
            .Include(x => x.ServiceLineActions)
                .ThenInclude(sla => sla.Person)
            .Include(x => x.ServiceLineActions)
                .ThenInclude(sla => sla.Customer)
            .Include(x => x.ServiceLineActions)
                .ThenInclude(sla => sla.Status)
            .FirstOrDefaultAsync(cancellationToken);

        if (sa is null)
            return Result<ServiceAction>.Failure("Service Action bulunamadı veya silinmiş.");

        return Result<ServiceAction>.Succeed(sa);
    }
}
