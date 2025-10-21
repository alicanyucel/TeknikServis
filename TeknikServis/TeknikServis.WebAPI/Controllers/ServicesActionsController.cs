using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeknikServis.Application.Features.ServiceActions.CreateServiceActions;
using TeknikServis.Application.Features.ServiceActions.DeleteServiceActions;
using TeknikServis.Application.Features.ServiceActions.GetAllServiceActions;
using TeknikServis.Application.Features.ServiceActions.GetByIdServiceActions;
using TeknikServis.Application.Features.ServiceActions.UpdateServiceAcrions;
using TeknikServis.WebAPI.Abstractions;

namespace TeknikServis.WebAPI.Controllers;

[AllowAnonymous]
public class ServicesActionsController : ApiController
{
    public ServicesActionsController(IMediator mediator) : base(mediator)
    {
    }
    [HttpPost]
    public async Task<IActionResult> CreateServiceActions(CreateServiceActionCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return NoContent();
    }


    [HttpPost]
    public async Task<IActionResult> ServiceActionGetById(GetServiceActionByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);


    }
    [HttpPost]
    public async Task<IActionResult> ServiceActionDelete(DeleteServiceActionsCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> GetAllServiceActions(GetAllServiceActionQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
    [HttpPost]
    public async Task<IActionResult> UpdateServiceAction(UpdateServiceActionCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}
