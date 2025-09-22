using MediatR;
using TeknikServis.Domain.Entities;
using TS.Result;

namespace TeknikServis.Application.Features.ServiceLineActions.GetAllServiceLineAction;

public sealed record GetAllServiceLineActionQuery : IRequest<Result<List<ServiceLineAction>>>;
