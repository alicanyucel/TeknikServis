using MediatR;
using TS.Result;

namespace TeknikServis.Application.Features.Districts.SetDistrict;

public sealed record SetDistrictCommand() : IRequest<Result<string>>;