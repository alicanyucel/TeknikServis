using MediatR;
using TS.Result;

namespace TeknikServis.Application.Features.Persons.DeletePerson;

public sealed record DeletePersonCommand(Guid Id) : IRequest<Result<string>>;
