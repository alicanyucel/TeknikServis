using GenericRepository;
using MediatR;
using TeknikServis.Application.Constanst;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Provinces.SetProvince;

internal sealed class SetProvinceCommandHandler(IProvinceRepository provinceRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<SetProvinceCommand, Result<string>>
{
    public async Task<Result<string>> Handle(SetProvinceCommand request, CancellationToken cancellationToken)
    {
        // ProvinceConstant üzerinden senkronizasyon (81 il)
        var provinces = ProvinceConstant.Provinces
            .Select((name, index) => new Province
            {
                Id = index + 1,
                Name = name,
                CountryId = 1,
                CreatedBy = "system",
                UpdatedBy = "system",
                CreateadAt = DateTime.UtcNow,
                CreatedTime = new TimeOnly(0, 0),
                UpdatedTime = new TimeOnly(0, 0)
            })
            .ToList();

        foreach (var item in provinces)
        {
            var existing = await provinceRepository.GetByExpressionWithTrackingAsync(p => p.Id == item.Id, cancellationToken);
            if (existing is null)
            {
                await provinceRepository.AddAsync(item, cancellationToken);
            }
            else
            {
                existing.Name = item.Name;
                existing.CountryId = item.CountryId;
                existing.UpdatedBy = "system";
                existing.UpdatedTime = new TimeOnly(0, 0);
                provinceRepository.Update(existing);
            }
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Provinces senkronize edildi");
    }
}
