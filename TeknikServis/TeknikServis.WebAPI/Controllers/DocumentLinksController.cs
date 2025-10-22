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
            return BadRequest("Dosya seçilmedi.");

        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { message = "Döküman kaydı yapıldı." })
            : BadRequest(new { message = "Döküman kaydı başarısız.", errors = result.ErrorMessages });
    }

    [HttpPost]
    public async Task<IActionResult> DocumentLinkGetById(GetByIdDocumentinkQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> DocumentLinkDelete(DeleteDocumentLinkCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> GetAllDocumentLink(GetAllDocumentLinkQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UpdateDocumentLink([FromForm] UpdateDocumentLinkCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}
