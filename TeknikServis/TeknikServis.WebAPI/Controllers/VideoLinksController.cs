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
[Produces("application/json")]
public class VideoLinksController : ApiController
{
    public VideoLinksController(IMediator mediator) : base(mediator) { }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> CreateVideoLinks([FromForm] CreateVideoLinkCommand request, CancellationToken cancellationToken)
    {
        if ((request.File is null || request.File.Length == 0))
            return BadRequest(new { success = false, message = "A file must be provided." });

        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "Video uploaded and saved successfully." })
            : BadRequest(new { success = false, message = "Failed to create video link.", errors = result.ErrorMessages });
    }

    [HttpPost]
    public async Task<IActionResult> VideoLinkGetById(GetVideoLinkByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "Video link retrieved.", data = result.Data })
            : NotFound(new { success = false, message = "Video link not found.", errors = result.ErrorMessages });
    }

    [HttpPost]
    public async Task<IActionResult> VideoLinkDelete(DeleteVideoLinkCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "Video link deleted." })
            : BadRequest(new { success = false, message = "Failed to delete video link.", errors = result.ErrorMessages });
    }

    [HttpPost]
    public async Task<IActionResult> GetAllVideoLinks(GetAllVideoLinkQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "Video links listed.", data = result.Data })
            : BadRequest(new { success = false, message = "Failed to list video links.", errors = result.ErrorMessages });
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UpdateVideoLinks([FromForm] UpdateVideoLinkCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "Video link updated." })
            : BadRequest(new { success = false, message = "Failed to update video link.", errors = result.ErrorMessages });
    }
}
