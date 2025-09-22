using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeknikServis.Application.Features.VideoLinks.CrateVideoLinks;
using TeknikServis.WebAPI.Abstractions;


namespace TeknikServis.WebAPI.Controllers;

[AllowAnonymous]
public class VideoLinksController : ApiController
{
    public VideoLinksController(IMediator mediator) : base(mediator)
    {
    }
    [HttpPost]
    public async Task<IActionResult> CreateVideoLink(CreateVideoLinkCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return NoContent();
    }
}
