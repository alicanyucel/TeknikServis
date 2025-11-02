using MediatR;
using TS.Result;

namespace TeknikServis.Application.Features.Provinces.SetProvince;

public sealed record SetProvinceCommand : IRequest<Result<string>>;
