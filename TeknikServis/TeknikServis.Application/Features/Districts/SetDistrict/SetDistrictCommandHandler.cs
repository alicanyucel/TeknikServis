using GenericRepository;
using MediatR;
using TeknikServis.Application.Constanst;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Districts.SetDistrict;

internal sealed class SetDistrictCommandHandler(IDistrictRepository districtRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<SetDistrictCommand, Result<string>>
{
    public async Task<Result<string>> Handle(SetDistrictCommand request, CancellationToken cancellationToken)
    {
        // DistrictConstant üzerinden senkronizasyon (tüm ilçeler)
        var districts = DistrictConstant.Districts
            .Select((d, index) => new District
            {
                Id = index + 1,
                Name = d.DistrictName,
                ProvinceId = d.ProvinceId,
                CreatedBy = "system",
                UpdatedBy = "system",
                CreateadAt = DateTime.UtcNow,
                CreatedTime = new TimeOnly(0, 0),
                UpdatedTime = new TimeOnly(0, 0)
            })
            .ToList();

        foreach (var item in districts)
        {
            var existing = await districtRepository.GetByExpressionWithTrackingAsync(d => d.Id == item.Id, cancellationToken);
            if (existing is null)
            {
                await districtRepository.AddAsync(item, cancellationToken);
            }
            else
            {
                existing.Name = item.Name;
                existing.ProvinceId = item.ProvinceId;
                existing.UpdatedBy = "system";
                existing.UpdatedTime = new TimeOnly(0, 0);
                districtRepository.Update(existing);
            }
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Districts senkronize edildi");
    }
}