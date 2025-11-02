using MediatR;
using TS.Result;

namespace TeknikServis.Application.Features.Countries.SetCountry;

public sealed record SetCountryCommand() : IRequest<Result<string>>;