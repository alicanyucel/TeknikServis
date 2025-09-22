using MediatR;
using TS.Result;

namespace TeknikServis.Application.Features.ServiceLineActions.DeleteServiceLineActions;

public sealed record DeleteServiceLineActionCommand(Guid Id) : IRequest<Result<string>>;
