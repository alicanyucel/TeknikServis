using GenericRepository;
using MediatR;
using TeknikServis.Application.Constanst;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Districts.SetDistrict;

internal sealed class SetDistrictCommandHandler(IDistrictRepository districtRepository, IProvinceRepository provinceRepository, ICountryRepository countryRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<SetDistrictCommand, Result<string>>
{
    public async Task<Result<string>> Handle(SetDistrictCommand request, CancellationToken cancellationToken)
    {
        // Ülkeyi (Türkiye) garanti altýna al
        var country = await countryRepository.GetByExpressionWithTrackingAsync(c => c.Name == CountryConstants.Türkiye.Name, cancellationToken);
        if (country is null)
        {
            country = new Country
            {
                Name = CountryConstants.Türkiye.Name,
                Code = CountryConstants.Türkiye.Code,
                CreatedBy = "system",
                UpdatedBy = "system",
                CreateadAt = DateTime.UtcNow,
                CreatedTime = new TimeOnly(0, 0),
                UpdatedTime = new TimeOnly(0, 0)
            };
            await countryRepository.AddAsync(country, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

      
        var provinceNameToId = new Dictionary<string, int>();
        foreach (var name in ProvinceConstant.Provinces)
        {
            var prov = await provinceRepository.GetByExpressionWithTrackingAsync(p => p.Name == name, cancellationToken);
            if (prov is null)
            {
                prov = new Province
                {
                    Name = name,
                    CountryId = country.Id,
                    CreatedBy = "system",
                    UpdatedBy = "system",
                    CreateadAt = DateTime.UtcNow,
                    CreatedTime = new TimeOnly(0, 0),
                    UpdatedTime = new TimeOnly(0, 0)
                };
                await provinceRepository.AddAsync(prov, cancellationToken);
                await unitOfWork.SaveChangesAsync(cancellationToken);
            }
            provinceNameToId[name] = prov.Id;
        }

       
        foreach (var (provinceIndex1Based, districtName) in DistrictConstant.Districts)
        {
            var provinceName = ProvinceConstant.Provinces[provinceIndex1Based - 1];
            var provinceId = provinceNameToId[provinceName];

            var existing = await districtRepository.GetByExpressionWithTrackingAsync(d => d.Name == districtName && d.ProvinceId == provinceId, cancellationToken);
            if (existing is null)
            {
                var entity = new District
                {
                    Name = districtName,
                    ProvinceId = provinceId,
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
                existing.UpdatedBy = "system";
                existing.UpdatedTime = new TimeOnly(0, 0);
                districtRepository.Update(existing);
            }
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Districts senkronize edildi");
    }
}