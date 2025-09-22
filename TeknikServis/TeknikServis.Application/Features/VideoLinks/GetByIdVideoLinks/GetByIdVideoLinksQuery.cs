using MediatR;
using TeknikServis.Domain.Entities;
using TS.Result;

namespace TeknikServis.Application.Features.VideoLinks.GetByIdVideoLinks;

public sealed record GetVideoLinkByIdQuery(Guid Id) : IRequest<Result<VideoLink>>;