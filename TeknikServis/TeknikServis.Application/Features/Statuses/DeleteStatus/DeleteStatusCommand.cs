using MediatR;
using TS.Result;

namespace TeknikServis.Application.Features.Statuses.DeleteStatus;

public sealed record DeleteStatusCommand(Guid Id) : IRequest<Result<string>>;
