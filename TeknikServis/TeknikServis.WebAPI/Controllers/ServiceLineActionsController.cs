using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeknikServis.Application.Features.ServiceLineActions.CreateServiceLineActions;
using TeknikServis.Application.Features.ServiceLineActions.DeleteServiceLineActions;
using TeknikServis.Application.Features.ServiceLineActions.GetAllServiceLineAction;
using TeknikServis.Application.Features.ServiceLineActions.GetByIdServiceLineAction;
using TeknikServis.Application.Features.ServiceLineActions.UpdateServiceLineActions;
using TeknikServis.WebAPI.Abstractions;

namespace TeknikServis.WebAPI.Controllers;
[AllowAnonymous]
[Produces("application/json")]
public class ServiceLineActionsController : ApiController
{
    public ServiceLineActionsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> CreateServiceLineAction(CreateServiceLineActionsCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "Service line action created." })
            : BadRequest(new { success = false, message = "Failed to create service line action.", errors = result.ErrorMessages });
    }

    [HttpPost]
    public async Task<IActionResult> ServiceLineActionGetById(GetServiceLineActionByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "Service line action retrieved.", data = result.Data })
            : NotFound(new { success = false, message = "Service line action not found.", errors = result.ErrorMessages });
    }

    [HttpPost]
    public async Task<IActionResult> ServiceLineActionDelete(DeleteServiceLineActionCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "Service line action deleted." })
            : BadRequest(new { success = false, message = "Failed to delete service line action.", errors = result.ErrorMessages });
    }

    [HttpPost]
    public async Task<IActionResult> GetAllServiceLineAction(GetAllServiceLineActionQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "Service line actions listed.", data = result.Data })
            : BadRequest(new { success = false, message = "Failed to list service line actions.", errors = result.ErrorMessages });
    }

    [HttpPost]
    public async Task<IActionResult> UpdateServiceLineAction(UpdateServiceLineActionsCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "Service line action updated." })
            : BadRequest(new { success = false, message = "Failed to update service line action.", errors = result.ErrorMessages });
    }
}
