using GenericRepository;
using MediatR;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Countries.SetCountry;

internal sealed class SetCountryCommandHandler(ICountryRepository countryRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<SetCountryCommand, Result<string>>
{
    public async Task<Result<string>> Handle(SetCountryCommand request, CancellationToken cancellationToken)
    {
        var country = await countryRepository.GetByExpressionWithTrackingAsync(x => x.Id == request.Id, cancellationToken);
        if (country is null)
        {
            country = new Country
            {
                Id = request.Id,
                Name = request.Name,
                Code = request.Code,
                CreatedBy = "system",
                UpdatedBy = "system",
                CreateadAt = DateTime.UtcNow,
                CreatedTime = new TimeOnly(0, 0),
                UpdatedTime = new TimeOnly(0, 0)
            };
            await countryRepository.AddAsync(country, cancellationToken);
        }
        else
        {
            country.Name = request.Name;
            country.Code = request.Code;
            country.UpdatedBy = "system";
            country.UpdatedTime = new TimeOnly(0, 0);
            countryRepository.Update(country);
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Country set edildi");
    }
}