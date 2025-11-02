using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeknikServis.Application.Features.Countries.GetAllCountries;
using TeknikServis.Application.Features.Countries.SetCountry;
using TeknikServis.WebAPI.Abstractions;

namespace TeknikServis.WebAPI.Controllers;

[AllowAnonymous]
public class CountriesController : ApiController
{
    public CountriesController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> SetCountry(SetCountryCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "ülkeler eklendi" })
            : BadRequest(new { success = false, message = "Ülkeler eklenemedi", errors = result.ErrorMessages });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCountries(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllCountriesQuery(), cancellationToken);
        return result.IsSuccessful ? Ok(result.Data) : BadRequest(result.ErrorMessages);
    }
}
