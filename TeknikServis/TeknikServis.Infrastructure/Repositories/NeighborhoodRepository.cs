using GenericRepository;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TeknikServis.Infrastructure.Context;

namespace TeknikServis.Infrastructure.Repositories;

internal sealed class NeighborhoodRepository : Repository<Neighborhood, ApplicationDbContext>, INeighborhoodRepository
{
    public NeighborhoodRepository(ApplicationDbContext context) : base(context)
    {
    }
}
