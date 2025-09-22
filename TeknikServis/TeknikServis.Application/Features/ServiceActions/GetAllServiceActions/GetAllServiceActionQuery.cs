using MediatR;
using TeknikServis.Domain.Entities;
using TS.Result;

namespace TeknikServis.Application.Features.ServiceActions.GetAllServiceActions;

public sealed record GetAllServiceActionQuery : IRequest<Result<List<ServiceAction>>>;