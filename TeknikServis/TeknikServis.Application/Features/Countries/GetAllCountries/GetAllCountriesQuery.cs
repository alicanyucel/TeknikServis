using MediatR;
using TeknikServis.Domain.Entities;
using TS.Result;

namespace TeknikServis.Application.Features.Countries.GetAllCountries;

public sealed record GetAllCountriesQuery : IRequest<Result<List<Country>>>;