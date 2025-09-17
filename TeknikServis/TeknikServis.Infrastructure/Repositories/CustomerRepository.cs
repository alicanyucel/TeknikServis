using GenericRepository;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TeknikServis.Infrastructure.Context;

namespace TeknikServis.Infrastructure.Repositories;

internal sealed class CustomerRepository:Repository<Customer,ApplicationDbContext>,ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext context):base(context)
    {
        
    }
}

