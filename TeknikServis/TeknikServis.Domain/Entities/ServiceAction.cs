using TeknikServis.Domain.Abstractions;

namespace TeknikServis.Domain.Entities;

public sealed class ServiceAction : Entity
{
    public DateTime ActionDate { get; set; }
    public string Description { get; set; } = default!;
    public Guid PersonId { get; set; }
    public required Person Person { get; set; }
    public Guid StatusId { get; set; }
    public required Status Status { get; set; }
    public ICollection<DocumentLink> DocumentLinks { get; set; } = new List<DocumentLink>();
    public ICollection<VideoLink> VideoLinks { get; set; } = new List<VideoLink>();
    public Guid CustomerId { get; set; }
    public required Customer Customer { get; set; }
    public ICollection<ServiceLineAction> ServiceLineActions { get; set; } = new List<ServiceLineAction>();
}
