using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeknikServis.Application.Features.Provinces.GetAllProvinces;
using TeknikServis.Application.Features.Provinces.SetProvince;
using TeknikServis.WebAPI.Abstractions;

namespace TeknikServis.WebAPI.Controllers;

[AllowAnonymous]
public class ProvincesController : ApiController
{
    public ProvincesController(IMediator mediator) : base(mediator)
    {
    }
    [HttpPost]
    public async Task<IActionResult> SetProvince(SetProvinceCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "Provinces synced." })
            : BadRequest(new { success = false, message = "Failed to sync provinces.", errors = result.ErrorMessages });
    }
    [HttpPost]
    public async Task<IActionResult> GetAllProvinces(GetAllProvincesQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(new { success = true, message = "Provinces listed.", data = result });
    }
}
