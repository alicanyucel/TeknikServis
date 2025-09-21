using GenericRepository;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TeknikServis.Infrastructure.Context;

namespace TeknikServis.Infrastructure.Repositories;

internal sealed class DistrictRepository : Repository<District, ApplicationDbContext>, IDistrictRepository
{
    public DistrictRepository(ApplicationDbContext context) : base(context)
    {

    }
}
