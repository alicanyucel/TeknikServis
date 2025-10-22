using MediatR;
using Microsoft.AspNetCore.Http;
using TS.Result;

namespace TeknikServis.Application.Features.VideoLinks.CrateVideoLinks;

public sealed record CreateVideoLinkCommand(
    IFormFile? File,
    string? Url,
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