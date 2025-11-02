using MediatR;
using TeknikServis.Application.Constanst;
using TeknikServis.Application.Dtos;
using TS.Result;

namespace TeknikServis.Application.Features.Provinces.GetAllProvinces;

public sealed class GetAlllProvincesQueryHandler : IRequestHandler<GetAllProvincesQuery, Result<List<ProvinceListDto>>>
{
    public Task<Result<List<ProvinceListDto>>> Handle(GetAllProvincesQuery request, CancellationToken cancellationToken)
    {
        var provinces = ProvinceConstant.Provinces
            .Select((name, index) => new ProvinceListDto(
                Id: index + 1,
                Name: name,
                CountryId: 1))
            .ToList();

        return Task.FromResult(Result<List<ProvinceListDto>>.Succeed(provinces));
    }
}
