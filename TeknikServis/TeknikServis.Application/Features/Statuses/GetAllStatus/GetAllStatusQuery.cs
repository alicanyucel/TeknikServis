using MediatR;
using TeknikServis.Application.Dtos;
using TS.Result;

namespace TeknikServis.Application.Features.Statuses.GetAllStatus;

public sealed record GetAllStatusQuery() : IRequest<Result<List<StatusDto>>>;
