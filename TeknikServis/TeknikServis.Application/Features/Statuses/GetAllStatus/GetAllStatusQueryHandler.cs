using MediatR;
using TeknikServis.Application.Dtos;
using TeknikServis.Application.Extensions;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Statuses.GetAllStatus;

public sealed class GetAllStatusQueryHandler : IRequestHandler<GetAllStatusQuery, Result<List<StatusDto>>>
{
    private readonly IStatusRepository _repository;

    public GetAllStatusQueryHandler(IStatusRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<StatusDto>>> Handle(GetAllStatusQuery request, CancellationToken cancellationToken)
    {
        var statuses = await _repository.GetAllAsync(cancellationToken);
        if (statuses is null || statuses.Count == 0) 
        return Result<List<StatusDto>>.Failure("Hiç durum bulunamadı.");
        var dtoList = statuses.Select(s => new StatusDto(s.Id, s.Name)).ToList();
        return Result<List<StatusDto>>.Succeed(dtoList); 
    }
}
