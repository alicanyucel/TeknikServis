using MediatR;
using TS.Result;

namespace TeknikServis.Application.Features.Auth.Register;

public sealed record RegisterCommand(
    string EmailOrUserName,
    string Password,
    string RePassword) : IRequest<Result<RegisterCommandResponse>>;
