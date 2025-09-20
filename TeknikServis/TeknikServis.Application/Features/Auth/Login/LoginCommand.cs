using MediatR;
using TS.Result;

namespace TeknikServis.Application.Features.Auth.Login;

public sealed record LoginCommand(
    string EmailOrUserName,
    string Password) : IRequest<Result<LoginCommandResponse>>;
//