using MediatR;
using TeknikServis.Domain.Entities;
using TS.Result;

namespace TeknikServis.Application.Features.Persons.GetAllPerson;

public sealed record GetAllPersonQuery : IRequest<Result<List<Person>>>;
