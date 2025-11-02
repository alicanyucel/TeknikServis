using MediatR;
using TeknikServis.Application.Dtos;
using TS.Result;

namespace TeknikServis.Application.Features.Provinces.GetAllProvinces;

public sealed record GetAllProvincesQuery : IRequest<Result<List<ProvinceListDto>>>;
