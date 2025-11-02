using GenericRepository;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TeknikServis.Infrastructure.Context;

namespace TeknikServis.Infrastructure.Repositories;

internal sealed class CountryRepository : Repository<Country, ApplicationDbContext>, ICountryRepository
{
    public CountryRepository(ApplicationDbContext context) : base(context)
    {

    }
}

