using MediatR;
using TeknikServis.Domain.Entities;
using TS.Result;

namespace TeknikServis.Application.Features.Persons.GetByIdPerson;

public sealed record GetPersonByIdQuery(Guid Id) : IRequest<Result<Person>>;
