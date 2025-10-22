using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeknikServis.Application.Features.Roles.CreateRole;
using TeknikServis.Application.Features.Roles.GetAllRoles;
using TeknikServis.WebAPI.Abstractions;

namespace TeknikServis.WebAPI.Controllers;

[AllowAnonymous]
[Produces("application/json")]
public class RolesController : ApiController
{
    public RolesController(IMediator mediator) : base(mediator)
    {
    }
    [HttpPost]
    public async Task<IActionResult> CreateRole(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "Roles synced." })
            : BadRequest(new { success = false, message = "Failed to sync roles.", errors = result.ErrorMessages });
    }
    [HttpPost]
    public async Task<IActionResult> GetAllRoles(GetAllRoleQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(new { success = true, message = "Roles listed.", data = result });
    }
}
