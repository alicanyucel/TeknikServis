using MediatR;
using TS.Result;

namespace TeknikServis.Application.Features.Users.UpdateUser;


public sealed record UpdateUserCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Password,
    IList<string> Roles,
    TimeOnly UpdatedTime,
    string UpdatedBy,
    string CreatedBy,
    TimeOnly CratedTime,
    DateTime CreateadAt,
    DateTime? UpdatedAt,
    bool IsDeleted
) : IRequest<Result<string>>;

