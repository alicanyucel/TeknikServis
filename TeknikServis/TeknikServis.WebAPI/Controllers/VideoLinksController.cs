using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeknikServis.Application.Features.VideoLinks.CrateVideoLinks;
using TeknikServis.Application.Features.VideoLinks.DeleteVideoLinks;
using TeknikServis.Application.Features.VideoLinks.GetAllVideoLinks;
using TeknikServis.Application.Features.VideoLinks.GetByIdVideoLinks;
using TeknikServis.Application.Features.VideoLinks.UpdateVideoLinks;
using TeknikServis.WebAPI.Abstractions;


namespace TeknikServis.WebAPI.Controllers;

[AllowAnonymous]
public class VideoLinksController : ApiController
{
    public VideoLinksController(IMediator mediator) : base(mediator)
    {
    }
    [HttpPost]
    public async Task<IActionResult> CreateVideoLinks(CreateVideoLinkCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return NoContent();
    }


    [HttpPost]
    public async Task<IActionResult> VideoLinkGetById(GetVideoLinkByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);


    }
    [HttpPost]
    public async Task<IActionResult> VideoLinkDelete(DeleteVideoLinkCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> GetAllVideoLinks(GetAllVideoLinkQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
    [HttpPost]
    public async Task<IActionResult> UpdateVideoLinks(UpdateVideoLinkCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}
