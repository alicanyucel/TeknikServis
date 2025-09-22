using MediatR;
using TS.Result;

namespace TeknikServis.Application.Features.VideoLinks.UpdateVideoLinks;

public sealed record UpdateVideoLinkCommand(
    Guid Id,
    string Url,
    string Description,
    Guid ServiceActionId,
    TimeOnly UpdatedTime,
    string UpdatedBy,
    string CreatedBy,
    TimeOnly CratedTime,
    DateTime CreateadAt,
    DateTime? UpdatedAt,
    bool IsDeleted
) : IRequest<Result<string>>;