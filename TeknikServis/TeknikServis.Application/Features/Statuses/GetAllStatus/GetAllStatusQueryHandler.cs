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

        var activeStatuses = statuses
            .Where(s => !s.IsDeleted)
            .Select(s => new StatusDto(
                Id: s.Id,
                Name: s.Name,
                UpdatedTime: s.UpdatedTime,
                UpdatedBy: s.UpdatedBy,
                CreatedBy: s.CreatedBy,
                CratedTime: s.CreatedTime,
                CreateadAt: s.CreateadAt,
                UpdatedAt: s.UpdatedAt,
                IsDeleted: s.IsDeleted
            ))
            .ToList();

        if (activeStatuses.Count == 0)
            return Result<List<StatusDto>>.Failure("Hiç aktif durum bulunamadı.");

        return Result<List<StatusDto>>.Succeed(activeStatuses);
    }
}
