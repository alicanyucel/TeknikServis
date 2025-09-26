using AutoMapper;
using MediatR;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.VideoLinks.GetByIdVideoLinks;

public sealed class GetVideoLinksByIdQueryHandler : IRequestHandler<GetVideoLinkByIdQuery, Result<VideoLink>>
{
    private readonly IVideoLinkRepository _videoLinkRepository;
    private readonly IMapper _mapper;

    public GetVideoLinksByIdQueryHandler(IVideoLinkRepository videoLinkRepository, IMapper mapper)
    {
        _videoLinkRepository = videoLinkRepository;
        _mapper = mapper;
    }

    public async Task<Result<VideoLink>> Handle(GetVideoLinkByIdQuery request, CancellationToken cancellationToken)
    {
        var videoLinkEntity = await _videoLinkRepository.GetByExpressionAsync(
            x => x.Id == request.Id && !x.IsDeleted,
            cancellationToken
        );

        if (videoLinkEntity is null)
            return Result<VideoLink>.Failure("Video Link bulunamadı veya silinmiş.");

        var videoLink = _mapper.Map<VideoLink>(videoLinkEntity);
        return Result<VideoLink>.Succeed(videoLink);
    }
}
