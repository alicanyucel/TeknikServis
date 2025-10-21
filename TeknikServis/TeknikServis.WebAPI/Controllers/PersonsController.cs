using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeknikServis.Application.Constanst;
using TeknikServis.Application.Features.Persons.CreatePerson;
using TeknikServis.Application.Features.Persons.DeletePerson;
using TeknikServis.Application.Features.Persons.GetAllPerson;
using TeknikServis.Application.Features.Persons.GetByIdPerson;
using TeknikServis.Application.Features.Persons.UpdatePerson;
using TeknikServis.WebAPI.Abstractions;

public class PersonsController : ApiController
{
    public PersonsController(IMediator mediator) : base(mediator) { }

    [HttpPost]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<IActionResult> CreatePersonel(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { message = "Personel başarıyla oluşturuldu.", personelId = result.Data })
            : BadRequest(new { message = "Personel oluşturulamadı.", errors = result.ErrorMessages });
    }

    [HttpPost]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<IActionResult> GetPersonelById(GetPersonByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result != null
            ? Ok(result)
            : NotFound(new { message = "Personel bulunamadı." });
    }

    [HttpPost]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<IActionResult> DeletePersonel(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { message = "Personel başarıyla silindi." })
            : BadRequest(new { message = "Personel silinemedi.", errors = result.ErrorMessages });
    }

    [HttpPost]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<IActionResult> GetAllPersonel(GetAllPersonQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(new { message = "Personel listesi getirildi.", data = result });
    }

    [HttpPost]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<IActionResult> UpdatePersonel(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { message = "Personel başarıyla güncellendi." })
            : BadRequest(new { message = "Personel güncellenemedi.", errors = result.ErrorMessages });
    }
}

