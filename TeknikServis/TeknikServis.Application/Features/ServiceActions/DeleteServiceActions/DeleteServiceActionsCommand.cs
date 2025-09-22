using MediatR;
using TS.Result;

namespace TeknikServis.Application.Features.ServiceActions.DeleteServiceActions;

public sealed record DeleteServiceActionsCommand(Guid Id) : IRequest<Result<string>>;