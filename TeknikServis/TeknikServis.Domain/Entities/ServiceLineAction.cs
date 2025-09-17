using TeknikServis.Domain.Abstractions;
namespace TeknikServis.Domain.Entities;

public sealed class ServiceLineAction : Entity
{
    public Guid ServiceActionId { get; set; }
    public required ServiceAction ServiceAction { get; set; }

    public DateTime ActionDate { get; set; } = DateTime.Now;

    public Guid PersonId { get; set; }
    public required Person Person { get; set; }

    public Guid ProductId { get; set; }
    public required Product Product { get; set; }

    public Guid CustomerId { get; set; }
    public required Customer Customer { get; set; }

    public string Description { get; set; } = default!;

    public Guid StatusId { get; set; }
    public required Status Status { get; set; }
}