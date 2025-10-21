using MediatR;
using TeknikServis.Application.Dtos;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Statuses.GetByIdStatus;

public sealed class GetStatusByIdQueryHandler : IRequestHandler<GetStatusByIdQuery, Result<StatusDto>>
{
    private readonly IStatusRepository _repository;

    public GetStatusByIdQueryHandler(IStatusRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<StatusDto>> Handle(GetStatusByIdQuery request, CancellationToken cancellationToken)
    {
        var s = await _repository.GetByExpressionAsync(
            x => x.Id == request.Id && !x.IsDeleted,
            cancellationToken
        );

        if (s is null)
            return Result<StatusDto>.Failure("Belirtilen ID ile eşleşen aktif durum bulunamadı.");

        var dto = new StatusDto(
            Id: s.Id,
            Name: s.Name,
            UpdatedTime: s.UpdatedTime,
            UpdatedBy: s.UpdatedBy,
            CreatedBy: s.CreatedBy,
            CratedTime: s.CreatedTime,
            CreateadAt: s.CreateadAt,
            UpdatedAt: s.UpdatedAt,
            IsDeleted: s.IsDeleted
        );
        return Result<StatusDto>.Succeed(dto);
    }
}
