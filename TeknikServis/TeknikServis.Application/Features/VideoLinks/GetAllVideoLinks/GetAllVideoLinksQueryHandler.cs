using MediatR;
using Microsoft.EntityFrameworkCore;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.VideoLinks.GetAllVideoLinks;

internal sealed class GetAllVideoLinkQueryHandler : IRequestHandler<GetAllVideoLinkQuery, Result<List<VideoLink>>>
{
    private readonly IVideoLinkRepository _videoLinkRepository;

    public GetAllVideoLinkQueryHandler(IVideoLinkRepository videoLinkRepository)
    {
        _videoLinkRepository = videoLinkRepository;
    }

    public async Task<Result<List<VideoLink>>> Handle(GetAllVideoLinkQuery request, CancellationToken cancellationToken)
    {
        var videoLinks = await _videoLinkRepository
            .GetAll()
            .Where(x => !x.IsDeleted)
            .ToListAsync(cancellationToken);

        return Result<List<VideoLink>>.Succeed(videoLinks);
    }
}
