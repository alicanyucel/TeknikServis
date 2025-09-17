using GenericRepository;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TeknikServis.Infrastructure.Context;

namespace TeknikServis.Infrastructure.Repositories;

internal sealed class VideoLinkRepository : Repository<VideoLink, ApplicationDbContext>, IVideoLinkRepository
{
    public VideoLinkRepository(ApplicationDbContext context) : base(context)
    {

    }
}
