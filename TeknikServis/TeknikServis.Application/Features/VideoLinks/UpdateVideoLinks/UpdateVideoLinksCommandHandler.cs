using AutoMapper;
using GenericRepository;
using MediatR;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.VideoLinks.UpdateVideoLinks;


internal sealed class UpdateVideoLinksCommandHandler(IVideoLinkRepository videoLinkRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateVideoLinkCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateVideoLinkCommand request, CancellationToken cancellationToken)
    {
        VideoLink? videoLink = await videoLinkRepository.GetByExpressionWithTrackingAsync(P => P.Id == request.Id, cancellationToken);
        if (videoLink == null)
        {
            return Result<string>.Failure("Video link bulunamadi.");
        }
        mapper.Map(request, videoLink);
        videoLinkRepository.Update(videoLink);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Video Link güncellendi.";
    }
}