using GenericRepository;
using MediatR;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.VideoLinks.DeleteVideoLinks;

public sealed class DeleteVideoLinksCommandHandler : IRequestHandler<DeleteVideoLinkCommand, Result<string>>
{
    private readonly IVideoLinkRepository _videoLinkRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteVideoLinksCommandHandler(IVideoLinkRepository videoLinkRepository, IUnitOfWork unitOfWork)
    {
        _videoLinkRepository = videoLinkRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteVideoLinkCommand request, CancellationToken cancellationToken)
    {
        var videolink = await _videoLinkRepository.GetByExpressionAsync(
            x => x.Id == request.Id,
            cancellationToken
        );

        if (videolink == null)
            return Result<string>.Failure("Video Link bulunamadı.");
        _videoLinkRepository.Delete(videolink);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return "Video Link silindi";
    }
}