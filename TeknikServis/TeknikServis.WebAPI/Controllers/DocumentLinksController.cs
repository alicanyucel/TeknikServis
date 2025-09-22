using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeknikServis.Application.Features.Customers.CreateCustomer;
using TeknikServis.Application.Features.Customers.CustomerGetById;
using TeknikServis.Application.Features.Customers.DeleteCustomers;
using TeknikServis.Application.Features.Customers.GetAllCustomers;
using TeknikServis.Application.Features.Customers.UpdateCustomer;
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
    public async Task<IActionResult> CreateDocumentLink(CreateDocumentLinkCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return NoContent();
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
    public async Task<IActionResult> UpdateDocumentLink(UpdateDocumentLinkCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}
