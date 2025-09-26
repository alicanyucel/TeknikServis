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
    public UsersController(IMediator mediator) : base(mediator)
    {
    }
    [HttpPost]
    [Authorize(Roles = RoleNames.Admin + "," + RoleNames.User + "," + RoleNames.Customer )]
    public async Task<IActionResult> CreateUser(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return NoContent();
    }


    [HttpPost]
    [Authorize(Roles = RoleNames.Admin + "," + RoleNames.User + "," + RoleNames.Customer)]
    public async Task<IActionResult> UserGetById(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);


    }
    [HttpPost]
    [Authorize(Roles = RoleNames.Admin + "," + RoleNames.User + "," + RoleNames.Customer)]
    public async Task<IActionResult> UserDelete(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);

        return NoContent();
    }

    [HttpPost]
    [Authorize(Roles = RoleNames.Admin + "," + RoleNames.User + "," + RoleNames.Customer)]
    public async Task<IActionResult> GetAllUsers(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
    [HttpPost]
    [Authorize(Roles = RoleNames.Admin + "," + RoleNames.User + "," + RoleNames.Customer)]
    public async Task<IActionResult> UpdateUser(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}
