using MediatR;
using TeknikServis.Application.Dtos;
using TeknikServis.Application.Extensions;
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
        var status = await _repository.GetByExpressionAsync(
            x => x.Id == request.Id && !x.IsDeleted,
            cancellationToken
        );

        if (status is null)
            return Result<StatusDto>.Failure("Belirtilen ID ile eşleşen aktif durum bulunamadı.");

        var dto = new StatusDto(status.Id, status.Name);
        return Result<StatusDto>.Succeed(dto);
    }
}
