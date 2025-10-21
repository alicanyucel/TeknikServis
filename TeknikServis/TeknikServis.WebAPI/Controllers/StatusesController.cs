using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeknikServis.Application.Features.Statuses.CreateSratus;
using TeknikServis.Application.Features.Statuses.DeleteStatus;
using TeknikServis.Application.Features.Statuses.GetAllStatus;
using TeknikServis.Application.Features.Statuses.GetByIdStatus;
using TeknikServis.Application.Features.Statuses.UpdateStatus;
using TeknikServis.WebAPI.Abstractions;

namespace TeknikServis.WebAPI.Controllers;

[AllowAnonymous]
public class StatusesController : ApiController
{
    public StatusesController(IMediator mediator) : base(mediator) { }

    [HttpPost]
    public async Task<IActionResult> GetAllStatus(GetAllStatusQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { message = "Servis durumları listelendi.", data = result.Data })
            : BadRequest(new { message = "Durumlar listelenemedi: " + result.ErrorMessages });
    }

    [HttpPost]
    public async Task<IActionResult> GetByIdStatus(GetStatusByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { message = "Servis durumu bulundu.", data = result.Data })
            : NotFound(new { message = "Servis durumu bulunamadı." });
    }

    [HttpPost]
    public async Task<IActionResult> CreateStatus(CreateStatusCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { message = "Servis durumu başarıyla oluşturuldu." })
            : BadRequest(new { message = "Oluşturma başarısız: " + result.ErrorMessages });
    }

    [HttpPost]
    public async Task<IActionResult> UpdateStatus(UpdateStatusCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { message = "Servis durumu güncellendi." })
            : BadRequest(new { message = "Güncelleme başarısız: " + result.ErrorMessages });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteStatus(DeleteStatusCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { message = "Servis durumu silindi." })
            : BadRequest(new { message = "Silme işlemi başarısız: " + result.ErrorMessages });
    }
}

