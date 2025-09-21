using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeknikServis.Application.Features.Statuses.CreateSratus;
using TeknikServis.Application.Features.Statuses.GetAllStatus;
using TeknikServis.Application.Features.Statuses.GetByIdStatus;
using TeknikServis.WebAPI.Abstractions;

namespace TeknikServis.WebAPI.Controllers;

[AllowAnonymous]
public class StatusesController : ApiController
{
    public StatusesController(IMediator mediator) : base(mediator)
    {
    }
    [HttpPost]
    public async Task<IActionResult> GetAllStatus(GetAllStatusQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
    [HttpPost]
    public async Task<IActionResult> CreateStatus(CreateStatusCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return NoContent();
    }
    [HttpPost]
    public async Task<IActionResult> GetByIdStatus(GetStatusByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}
