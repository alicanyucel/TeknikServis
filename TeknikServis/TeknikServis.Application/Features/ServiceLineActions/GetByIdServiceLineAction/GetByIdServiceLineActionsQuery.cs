using MediatR;
using TeknikServis.Domain.Entities;
using TS.Result;

namespace TeknikServis.Application.Features.ServiceLineActions.GetByIdServiceLineAction;

public sealed record GetServiceLineActionByIdQuery(Guid Id) : IRequest<Result<ServiceLineAction>>;

