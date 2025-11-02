using GenericRepository;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TeknikServis.Infrastructure.Context;

namespace TeknikServis.Infrastructure.Repositories;

internal sealed class ProvinceRepository : Repository<Province, ApplicationDbContext>, IProvinceRepository
{
    public ProvinceRepository(ApplicationDbContext context) : base(context)
    {

    }
}
