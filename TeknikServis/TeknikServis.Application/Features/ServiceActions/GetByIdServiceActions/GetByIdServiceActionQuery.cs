using MediatR;
using TeknikServis.Domain.Entities;
using TS.Result;

namespace TeknikServis.Application.Features.ServiceActions.GetByIdServiceActions;

public sealed record GetServiceActionByIdQuery(Guid Id) : IRequest<Result<ServiceAction>>;
