using GenericRepository;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TeknikServis.Infrastructure.Context;

namespace TeknikServis.Infrastructure.Repositories;

internal sealed class PersonRepository : Repository<Person, ApplicationDbContext>, IPersonRepository
{
    public PersonRepository(ApplicationDbContext context) : base(context)
    {

    }
}
