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
     
        var items = new List<(string Name, string? Code)>
        {
            (CountryConstants.Türkiye.Name, CountryConstants.Türkiye.Code)
        };

        foreach (var (name, code) in items)
        {
            var existing = await countryRepository.GetByExpressionWithTrackingAsync(x => x.Name == name, cancellationToken);
            if (existing is null)
            {
                var entity = new Country
                {
                    Name = name,
                    Code = code,
                    CreatedBy = "system",
                    UpdatedBy = "system",
                    CreateadAt = DateTime.UtcNow,
                    CreatedTime = new TimeOnly(0, 0),
                    UpdatedTime = new TimeOnly(0, 0)
                };
                await countryRepository.AddAsync(entity, cancellationToken);
            }
            else
            {
                existing.Code = code;
                existing.UpdatedBy = "system";
                existing.UpdatedTime = new TimeOnly(0, 0);
                countryRepository.Update(existing);
            }
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Countries senkronize edildi");
    }
}