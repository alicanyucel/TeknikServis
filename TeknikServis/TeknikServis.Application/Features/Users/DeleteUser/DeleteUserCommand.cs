using MediatR;
using TS.Result;

namespace TeknikServis.Application.Features.Users.DeleteUser;

public sealed record DeleteUserCommand(Guid Id) : IRequest<Result<string>>;
