using GenericRepository;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TeknikServis.Infrastructure.Context;

namespace TeknikServis.Infrastructure.Repositories;

internal sealed class ServiceLineActionRepository : Repository<ServiceLineAction, ApplicationDbContext>, IServiceActionLineRepository
{
    public ServiceLineActionRepository(ApplicationDbContext context) : base(context)
    {

    }
}
