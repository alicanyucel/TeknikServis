using MediatR;
using TS.Result;

namespace TeknikServis.Application.Features.Countries.SetCountry;

public sealed record SetCountryCommand(int Id, string Name, string? Code) : IRequest<Result<string>>;