using GenericRepository;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TeknikServis.Infrastructure.Context;

namespace TeknikServis.Infrastructure.Repositories;

internal sealed class DocumentLinkRepository : Repository<DocumentLink, ApplicationDbContext>, IDocumentLinkRepository
{
    public DocumentLinkRepository(ApplicationDbContext context) : base(context)
    {

    }
}
