using MediatR;
using TS.Result;

namespace TeknikServis.Application.Features.Persons.CreatePerson;

public sealed record CreatePersonCommand(
    string Name,
    string LastName,
    int ExpertiseArea,
    TimeOnly UpdatedTime,
    string UpdatedBy,
    string CreatedBy,
    TimeOnly CratedTime,
    DateTime CreateadAt,
    DateTime? UpdatedAt,
    bool IsDeleted
) : IRequest<Result<string>>;

