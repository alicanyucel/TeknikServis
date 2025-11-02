using GenericRepository;
using MediatR;
using TeknikServis.Application.Constanst;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Countries.SetCountry;

internal sealed class SetCountryCommandHandler(ICountryRepository countryRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<SetCountryCommand, Result<string>>
{
    public async Task<Result<string>> Handle(SetCountryCommand request, CancellationToken cancellationToken)
    {
        // Sabitlerden (þimdilik sadece Türkiye) senkronize et
        var items = new List<Country>
        {
            new Country
            {
                Id = CountryConstants.Türkiye.Id,
                Name = CountryConstants.Türkiye.Name,
                Code = CountryConstants.Türkiye.Code,
                CreatedBy = "system",
                UpdatedBy = "system",
                CreateadAt = DateTime.UtcNow,
                CreatedTime = new TimeOnly(0, 0),
                UpdatedTime = new TimeOnly(0, 0)
            }
        };

        foreach (var item in items)
        {
            var existing = await countryRepository.GetByExpressionWithTrackingAsync(x => x.Id == item.Id, cancellationToken);
            if (existing is null)
            {
                await countryRepository.AddAsync(item, cancellationToken);
            }
            else
            {
                existing.Name = item.Name;
                existing.Code = item.Code;
                existing.UpdatedBy = "system";
                existing.UpdatedTime = new TimeOnly(0, 0);
                countryRepository.Update(existing);
            }
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Countries senkronize edildi");
    }
}