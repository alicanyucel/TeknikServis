using MediatR;

namespace TeknikServis.Application.Features.VideoLinks.DeleteVideoLinks;

public sealed record DeleteVideoLinkCommand(Guid Id) : IRequest;
