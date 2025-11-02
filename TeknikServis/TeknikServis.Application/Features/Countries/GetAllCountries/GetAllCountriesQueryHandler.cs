using MediatR;
using TeknikServis.Application.Constanst;
using TeknikServis.Domain.Entities;
using TS.Result;

namespace TeknikServis.Application.Features.Countries.GetAllCountries;

public sealed class GetAllCountriesQueryHandler : IRequestHandler<GetAllCountriesQuery, Result<List<Country>>>
{
    public Task<Result<List<Country>>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
    {
        var countries = new List<Country>
        {
            CountryConstants.Türkiye
        };
        return Task.FromResult(Result<List<Country>>.Succeed(countries));
    }
}
