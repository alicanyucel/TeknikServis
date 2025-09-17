using GenericRepository;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TeknikServis.Infrastructure.Context;

namespace TeknikServis.Infrastructure.Repositories;

internal sealed class StatusRepository : Repository<Status, ApplicationDbContext>, IStatusRepository
{
    public StatusRepository(ApplicationDbContext context) : base(context)
    {

    }
}
