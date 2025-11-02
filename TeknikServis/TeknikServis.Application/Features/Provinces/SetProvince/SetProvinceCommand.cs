using MediatR;
using TS.Result;

namespace TeknikServis.Application.Features.Provinces.SetProvince;

public sealed record SetProvinceCommand(int Id, string Name, int CountryId) : IRequest<Result<string>>;
