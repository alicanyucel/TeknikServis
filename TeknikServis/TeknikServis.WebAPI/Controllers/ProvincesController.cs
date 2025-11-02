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
        return result.IsSuccessful ? Ok(result) : BadRequest(result);
    }

    [HttpPost]
    public async Task<IActionResult> GetAllProvinces(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllProvincesQuery(), cancellationToken);
        return result.IsSuccessful ? Ok(result.Data) : BadRequest(result.ErrorMessages);
    }
}
