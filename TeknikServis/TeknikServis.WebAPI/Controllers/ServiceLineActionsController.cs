using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeknikServis.Application.Constanst;
using TeknikServis.Application.Features.ServiceLineActions.CreateServiceLineActions;
using TeknikServis.Application.Features.ServiceLineActions.DeleteServiceLineActions;
using TeknikServis.Application.Features.ServiceLineActions.GetAllServiceLineAction;
using TeknikServis.Application.Features.ServiceLineActions.GetByIdServiceLineAction;
using TeknikServis.Application.Features.ServiceLineActions.UpdateServiceLineActions;
using TeknikServis.WebAPI.Abstractions;

namespace TeknikServis.WebAPI.Controllers;
[AllowAnonymous]
public class ServiceLineActionsController : ApiController
{
    public ServiceLineActionsController(IMediator mediator) : base(mediator)
    {
    }
    [HttpPost]
    public async Task<IActionResult> CreateServiceLineAction(CreateServiceLineActionsCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return NoContent();
    }


    [HttpPost]
    public async Task<IActionResult> ServiceLineActionGetById(GetServiceLineActionByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);


    }
    [HttpPost]
    public async Task<IActionResult> ServiceLineActionDelete(DeleteServiceLineActionCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> GetAllServiceLineAction(GetAllServiceLineActionQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
    [HttpPost]
    public async Task<IActionResult> UpdateServiceLineAction(UpdateServiceLineActionsCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}
