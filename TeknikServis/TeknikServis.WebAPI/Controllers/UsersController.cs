using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeknikServis.Application.Constanst;
using TeknikServis.Application.Features.Users.CreateUser;
using TeknikServis.Application.Features.Users.DeleteUser;
using TeknikServis.Application.Features.Users.GetAllUser;
using TeknikServis.Application.Features.Users.GetByIdUser;
using TeknikServis.Application.Features.Users.UpdateUser;
using TeknikServis.WebAPI.Abstractions;

namespace TeknikServis.WebAPI.Controllers;

public class UsersController : ApiController
{
    public UsersController(IMediator mediator) : base(mediator) { }

    [HttpPost]
    [Authorize(Roles = RoleNames.Admin + "," + RoleNames.User)]
    public async Task<IActionResult> CreateUser(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { message = "Kullanıcı başarıyla eklendi." })
            : BadRequest(new { message = "Kullanıcı eklenemedi: " + result.ErrorMessages });
    }

    [HttpPost]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<IActionResult> GetUserById(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { message = "Kullanıcı bulundu.", data = result.Data })
            : NotFound(new { message = "Kullanıcı bulunamadı." });
    }

    [HttpPost]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<IActionResult> DeleteUser(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { message = "Kullanıcı silindi." })
            : BadRequest(new { message = "Kullanıcı silinemedi: " + result.ErrorMessages });
    }

    [HttpPost]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<IActionResult> GetAllUsers(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { message = "Kullanıcılar listelendi.", data = result.Data })
            : BadRequest(new { message = "Kullanıcılar listelenemedi: " + result.ErrorMessages });
    }

    [HttpPost]
    [Authorize(Roles = RoleNames.Admin + "," + RoleNames.User)]
    public async Task<IActionResult> UpdateUser(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { message = "Kullanıcı güncellendi." })
            : BadRequest(new { message = "Güncelleme başarısız: " + result.ErrorMessages });
    }
}

