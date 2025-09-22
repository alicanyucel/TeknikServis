using MediatR;
using TS.Result;

namespace TeknikServis.Application.Features.Persons.UpdatePerson;

public sealed record UpdatePersonCommand(
Guid Id,
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
