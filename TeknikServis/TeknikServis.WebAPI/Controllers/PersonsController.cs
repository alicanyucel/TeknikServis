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

[Produces("application/json")]
public class PersonsController : ApiController
{
    public PersonsController(IMediator mediator) : base(mediator) { }

    [HttpPost]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<IActionResult> CreatePersonel(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "Person created." })
            : BadRequest(new { success = false, message = "Failed to create person.", errors = result.ErrorMessages });
    }

    [HttpPost]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<IActionResult> GetPersonelById(GetPersonByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "Person retrieved.", data = result.Data })
            : NotFound(new { success = false, message = "Person not found.", errors = result.ErrorMessages });
    }

    [HttpPost]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<IActionResult> DeletePersonel(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "Person deleted." })
            : BadRequest(new { success = false, message = "Failed to delete person.", errors = result.ErrorMessages });
    }

    [HttpPost]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<IActionResult> GetAllPersonel(GetAllPersonQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "People listed.", data = result.Data })
            : BadRequest(new { success = false, message = "Failed to list people.", errors = result.ErrorMessages });
    }

    [HttpPost]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<IActionResult> UpdatePersonel(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "Person updated." })
            : BadRequest(new { success = false, message = "Failed to update person.", errors = result.ErrorMessages });
    }
}

