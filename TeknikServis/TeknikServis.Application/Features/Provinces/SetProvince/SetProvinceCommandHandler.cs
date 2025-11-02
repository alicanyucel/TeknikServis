using GenericRepository;
using MediatR;
using TeknikServis.Application.Constanst;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Provinces.SetProvince;

internal sealed class SetProvinceCommandHandler(IProvinceRepository provinceRepository, ICountryRepository countryRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<SetProvinceCommand, Result<string>>
{
    public async Task<Result<string>> Handle(SetProvinceCommand request, CancellationToken cancellationToken)
    {
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
        var countryId = country.Id;
        foreach (var name in ProvinceConstant.Provinces)
        {
            var existing = await provinceRepository.GetByExpressionWithTrackingAsync(p => p.Name == name, cancellationToken);
            if (existing is null)
            {
                var entity = new Province
                {
                    Name = name,
                    CountryId = countryId,
                    CreatedBy = "system",
                    UpdatedBy = "system",
                    CreateadAt = DateTime.UtcNow,
                    CreatedTime = new TimeOnly(0, 0),
                    UpdatedTime = new TimeOnly(0, 0)
                };
                await provinceRepository.AddAsync(entity, cancellationToken);
            }
            else
            {
                existing.CountryId = countryId;
                existing.UpdatedBy = "system";
                existing.UpdatedTime = new TimeOnly(0, 0);
                provinceRepository.Update(existing);
            }
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Provinces senkronize edildi");
    }
}
