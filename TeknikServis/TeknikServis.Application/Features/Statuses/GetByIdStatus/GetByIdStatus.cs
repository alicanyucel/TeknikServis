using MediatR;
using TeknikServis.Application.Dtos;
using TS.Result;

namespace TeknikServis.Application.Features.Statuses.GetByIdStatus;

public sealed record GetStatusByIdQuery(Guid Id) : IRequest<Result<StatusDto>>;
