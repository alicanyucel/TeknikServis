using MediatR;
using TS.Result;

namespace TeknikServis.Application.Features.Auth.AdminApproval;

public sealed record ApproveUserAsStandardCommand(Guid UserId) : IRequest<Result<string>>;
