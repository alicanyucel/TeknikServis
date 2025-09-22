using AutoMapper;
using GenericRepository;
using MediatR;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.VideoLinks.CrateVideoLinks;

internal sealed class CreateVideoLinkComamndHandler(IVideoLinkRepository videoLinkRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateVideoLinkCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateVideoLinkCommand request, CancellationToken cancellationToken)
    {
        VideoLink videolink = mapper.Map<VideoLink>(request);
        await videoLinkRepository.AddAsync(videolink, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Video Link kaydı yapıldı";
    }
}

