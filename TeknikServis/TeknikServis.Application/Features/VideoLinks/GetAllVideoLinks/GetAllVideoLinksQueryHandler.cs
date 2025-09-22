using MediatR;
using Microsoft.EntityFrameworkCore;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.VideoLinks.GetAllVideoLinks;

internal sealed class GetAllVideoLinkQueryHandler(IVideoLinkRepository videoLinkRepository) : IRequestHandler<GetAllVideoLinkQuery, Result<List<VideoLink>>>
{
    public async Task<Result<List<VideoLink>>> Handle(GetAllVideoLinkQuery request, CancellationToken cancellationToken)
    {
        List<VideoLink> videoLinks = await videoLinkRepository.GetAll().ToListAsync(cancellationToken);
        return videoLinks.ToList();
    }
}