using MediatR;
using TS.Result;

namespace TeknikServis.Application.Features.Districts.SetDistrict;

public sealed record SetDistrictCommand(int Id, string Name, int ProvinceId) : IRequest<Result<string>>;