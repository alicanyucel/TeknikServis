using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeknikServis.Application.Features.DocumentLinks.CreateDcumentLink;
using TeknikServis.Application.Features.DocumentLinks.DeleteDocumentLink;
using TeknikServis.Application.Features.DocumentLinks.GetAllDocumentLink;
using TeknikServis.Application.Features.DocumentLinks.GetByIdDocumentLink;
using TeknikServis.Application.Features.DocumentLinks.UpdateDocumentLink;
using TeknikServis.WebAPI.Abstractions;

namespace TeknikServis.WebAPI.Controllers;

[AllowAnonymous]
[Produces("application/json")]
public class DocumentLinksController : ApiController
{
    public DocumentLinksController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> CreateDocumentLink([FromForm] CreateDocumentLinkCommand request, CancellationToken cancellationToken)
    {
        if (request.File is null || request.File.Length == 0)
            return BadRequest(new { success = false, message = "No file was selected." });

        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "Document uploaded and saved successfully." })
            : BadRequest(new { success = false, message = "Failed to create document link.", errors = result.ErrorMessages });
    }

    [HttpPost]
    public async Task<IActionResult> DocumentLinkGetById(GetByIdDocumentinkQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "Document link retrieved.", data = result.Data })
            : NotFound(new { success = false, message = "Document link not found.", errors = result.ErrorMessages });
    }

    [HttpPost]
    public async Task<IActionResult> DocumentLinkDelete(DeleteDocumentLinkCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "Document link deleted." })
            : BadRequest(new { success = false, message = "Failed to delete document link.", errors = result.ErrorMessages });
    }

    [HttpPost]
    public async Task<IActionResult> GetAllDocumentLink(GetAllDocumentLinkQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "Document links listed.", data = result.Data })
            : BadRequest(new { success = false, message = "Failed to list document links.", errors = result.ErrorMessages });
    }


    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UpdateDocumentLink([FromForm] UpdateDocumentLinkCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "Document link updated." })
            : BadRequest(new { success = false, message = "Failed to update document link.", errors = result.ErrorMessages });
    }
}
