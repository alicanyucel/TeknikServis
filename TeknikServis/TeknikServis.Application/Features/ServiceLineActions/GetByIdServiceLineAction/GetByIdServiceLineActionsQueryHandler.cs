using AutoMapper;
using MediatR;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.ServiceLineActions.GetByIdServiceLineAction;

public sealed class GetServiceLineActionByIdQueryHandler : IRequestHandler<GetServiceLineActionByIdQuery, Result<ServiceLineAction>>
{
    private readonly IServiceLineActionsRepository _slacRepository;
    private readonly IMapper _mapper;

    public GetServiceLineActionByIdQueryHandler(IServiceLineActionsRepository slacRepository, IMapper mapper)
    {
        _slacRepository = slacRepository;
        _mapper = mapper;
    }

    public async Task<Result<ServiceLineAction>> Handle(GetServiceLineActionByIdQuery request, CancellationToken cancellationToken)
    {
        var slacEntity = await _slacRepository.GetByExpressionAsync(
            x => x.Id == request.Id && !x.IsDeleted,
            cancellationToken
        );

        if (slacEntity is null)
            return Result<ServiceLineAction>.Failure("ServiceLine Actions bulunamadı veya silinmiş.");

        var slac = _mapper.Map<ServiceLineAction>(slacEntity);
        return Result<ServiceLineAction>.Succeed(slac);
    }
}
