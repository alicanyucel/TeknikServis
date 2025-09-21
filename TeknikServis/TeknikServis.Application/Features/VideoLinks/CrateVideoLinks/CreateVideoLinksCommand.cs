using MediatR;
using TS.Result;

namespace TeknikServis.Application.Features.VideoLinks.CrateVideoLinks;

public sealed record CreateVideoLinkCommand(
    string Url,
    string Description,
    Guid ServiceActionId
) : IRequest<Result<string>>;