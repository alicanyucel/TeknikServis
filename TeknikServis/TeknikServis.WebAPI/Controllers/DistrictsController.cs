using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeknikServis.Application.Features.Districts.GetAllDistricts;
using TeknikServis.Application.Features.Districts.SetDistrict;
using TeknikServis.WebAPI.Abstractions;

namespace TeknikServis.WebAPI.Controllers;

[AllowAnonymous]
public class DistrictsController : ApiController
{
    public DistrictsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> SetDistrict(SetDistrictCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "İlçe eklendi." })
            : BadRequest(new { success = false, message = "İlçe eklenemedi", errors = result.ErrorMessages });
    }


    [HttpPost]
    public async Task<IActionResult> GetAllDistricts([FromQuery] int? provinceId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllDistrictsQuery(provinceId), cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "İlçeler listelendi.", data = result.Data })
            : BadRequest(new { success = false, message = "İlçeler listelenemedi", errors = result.ErrorMessages });
    }
}
