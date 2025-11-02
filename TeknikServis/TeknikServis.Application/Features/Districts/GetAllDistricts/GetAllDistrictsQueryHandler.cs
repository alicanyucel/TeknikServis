using MediatR;
using TeknikServis.Application.Constanst;
using TeknikServis.Application.Dtos;
using TS.Result;

namespace TeknikServis.Application.Features.Districts.GetAllDistricts;

public sealed class GetAllDistrictsQueryHandler : IRequestHandler<GetAllDistrictsQuery, Result<List<DistrictListDto>>>
{
    public Task<Result<List<DistrictListDto>>> Handle(GetAllDistrictsQuery request, CancellationToken cancellationToken)
    {
        var list = DistrictConstant.Districts
            .Where(d => request.ProvinceId == null || d.ProvinceId == request.ProvinceId)
            .Select((d, index) => new DistrictListDto(
                Id: index + 1,
                Name: d.DistrictName,
                ProvinceId: d.ProvinceId))
            .ToList();

        return Task.FromResult(Result<List<DistrictListDto>>.Succeed(list));
    }
}
