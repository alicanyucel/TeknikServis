using AutoMapper;
using MediatR;
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
        var serviceActionsEntity = await _serviceActionRepository.GetByExpressionAsync(
            x => x.Id == request.Id && !x.IsDeleted,
            cancellationToken
        );

        if (serviceActionsEntity is null)
            return Result<ServiceAction>.Failure("Service Action bulunamadı veya silinmiş.");

        var serviceActions = _mapper.Map<ServiceAction>(serviceActionsEntity);
        return Result<ServiceAction>.Succeed(serviceActions);
    }
}
