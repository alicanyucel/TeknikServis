using GenericRepository;
using MediatR;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Districts.SetDistrict;

internal sealed class SetDistrictCommandHandler(IDistrictRepository districtRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<SetDistrictCommand, Result<string>>
{
    public async Task<Result<string>> Handle(SetDistrictCommand request, CancellationToken cancellationToken)
    {
        var existing = await districtRepository.GetByExpressionWithTrackingAsync(d => d.Id == request.Id, cancellationToken);
        if (existing is null)
        {
            var entity = new District
            {
                Id = request.Id,
                Name = request.Name,
                ProvinceId = request.ProvinceId,
                CreatedBy = "system",
                UpdatedBy = "system",
                CreateadAt = DateTime.UtcNow,
                CreatedTime = new TimeOnly(0, 0),
                UpdatedTime = new TimeOnly(0, 0)
            };
            await districtRepository.AddAsync(entity, cancellationToken);
        }
        else
        {
            existing.Name = request.Name;
            existing.ProvinceId = request.ProvinceId;
            existing.UpdatedBy = "system";
            existing.UpdatedTime = new TimeOnly(0, 0);
            districtRepository.Update(existing);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("District set edildi");
    }
}