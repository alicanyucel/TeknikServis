public sealed record CreateDocumentLinkCommand : IRequest<Result<string>>, IBaseRequest, IEquatable<CreateDocumentLinkCommand>
{
    public string Url { get; init; }
    public string Description { get; init; }
    public Guid ServiceActionId { get; init; }
    public string CreatedBy { get; init; } 
    public string UpdatedBy { get; init; } 
}
