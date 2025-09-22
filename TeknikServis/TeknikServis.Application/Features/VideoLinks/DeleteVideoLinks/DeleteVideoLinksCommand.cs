using MediatR;
using TS.Result;

namespace TeknikServis.Application.Features.VideoLinks.DeleteVideoLinks;

public sealed record DeleteVideoLinkCommand(Guid Id) : IRequest<Result<string>>;
