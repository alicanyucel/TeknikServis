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
        var videoLink = await _videoLinkRepository.GetByExpressionAsync(
            x => x.Id == request.Id && !x.IsDeleted,
            cancellationToken
        );

        if (videoLink is null)
            return Result<string>.Failure("Video Link bulunamadı veya zaten silinmiş.");

        videoLink.IsDeleted = true;
        videoLink.UpdatedAt = DateTime.UtcNow;
        videoLink.UpdatedBy = "admin"; 

        _videoLinkRepository.Update(videoLink);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Video Link başarıyla silindi (soft delete).");
    }
}
