using MediatR;
using TeknikServis.Application.Dtos;
using TS.Result;

namespace TeknikServis.Application.Features.Districts.GetAllDistricts;

public sealed record GetAllDistrictsQuery(int? ProvinceId = null) : IRequest<Result<List<DistrictListDto>>>;