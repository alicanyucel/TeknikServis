using MediatR;
using TeknikServis.Domain.Entities;
using TS.Result;

namespace TeknikServis.Application.Features.VideoLinks.GetAllVideoLinks;

public sealed record GetAllVideoLinkQuery : IRequest<Result<List<VideoLink>>>;