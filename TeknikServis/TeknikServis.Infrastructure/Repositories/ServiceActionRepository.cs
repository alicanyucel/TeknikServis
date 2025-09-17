using GenericRepository;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TeknikServis.Infrastructure.Context;

namespace TeknikServis.Infrastructure.Repositories;

internal sealed class ServiceActionRepository : Repository<ServiceAction, ApplicationDbContext>, IServiceActionRepository
{
    public ServiceActionRepository(ApplicationDbContext context) : base(context)
    {

    }
}
