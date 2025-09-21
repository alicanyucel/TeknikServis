using TeknikServis.Domain.Abstractions;

namespace TeknikServis.Domain.Entities;

public sealed class DocumentLink : Entity
{
    public string Url { get; set; } = default!;
    public string Description { get; set; } = default!;

    public Guid ServiceActionId { get; set; }
    public required ServiceAction ServiceAction { get; set; }
}