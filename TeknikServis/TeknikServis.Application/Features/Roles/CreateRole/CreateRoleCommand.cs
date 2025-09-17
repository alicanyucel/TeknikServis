using MediatR;
using TS.Result;

namespace TeknikServis.Application.Features.Roles.CreateRole;

public sealed record CreateRoleCommand() : IRequest<Result<string>>;
