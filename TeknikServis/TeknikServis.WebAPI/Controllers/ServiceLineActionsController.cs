using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeknikServis.Application.Features.ServiceLineActions.CreateServiceLineActions;
using TeknikServis.WebAPI.Abstractions;

namespace TeknikServis.WebAPI.Controllers;
[AllowAnonymous]
public class ServiceLineActionsController : ApiController
{
    public ServiceLineActionsController(IMediator mediator) : base(mediator)
    {
    }
    [HttpPost]
    public async Task<IActionResult> CreateServiceLineActions(CreateServiceLineActionsCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}
