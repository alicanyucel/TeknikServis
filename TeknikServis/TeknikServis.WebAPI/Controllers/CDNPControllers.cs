using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeknikServis.Application.Dtos;
using TeknikServis.Application.Features.UploadJsonLoader;
using TeknikServis.WebAPI.Abstractions;

namespace TeknikServis.WebAPI.Controllers;

[AllowAnonymous]
public class CDNPControllers : ApiController
{
    public CDNPControllers(IMediator mediator) : base(mediator)
    {
    }
    [HttpPost("upload-json")]
    public async Task<IActionResult> UploadJson([FromForm] JsonUploadDto dto)
    {
        try
        {
            var result = await _mediator.Send(new UploadJsonCommand(dto.File));
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
