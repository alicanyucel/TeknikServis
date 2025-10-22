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
[Produces("application/json")]
public class ServicesActionsController : ApiController
{
    public ServicesActionsController(IMediator mediator) : base(mediator)
    {
    }
    [HttpPost]
    public async Task<IActionResult> CreateServiceActions(CreateServiceActionCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "Service action created." })
            : BadRequest(new { success = false, message = "Failed to create service action.", errors = result.ErrorMessages });
    }


    [HttpPost]
    public async Task<IActionResult> ServiceActionGetById(GetServiceActionByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "Service action retrieved.", data = result.Data })
            : NotFound(new { success = false, message = "Service action not found.", errors = result.ErrorMessages });
    }
    [HttpPost]
    public async Task<IActionResult> ServiceActionDelete(DeleteServiceActionsCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccessful
            ? Ok(new { success = true, message = "Service action deleted." })
            : BadRequest(new { success = false, message = "Failed to delete service action.", errors = result.ErrorMessages });
    }

    [HttpPost]
    public async Task<IActionResult> GetAllServiceActions(GetAllServiceActionQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "Service actions listed.", data = result.Data })
            : BadRequest(new { success = false, message = "Failed to list service actions.", errors = result.ErrorMessages });
    }
    [HttpPost]
    public async Task<IActionResult> UpdateServiceAction(UpdateServiceActionCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "Service action updated." })
            : BadRequest(new { success = false, message = "Failed to update service action.", errors = result.ErrorMessages });
    }
}
