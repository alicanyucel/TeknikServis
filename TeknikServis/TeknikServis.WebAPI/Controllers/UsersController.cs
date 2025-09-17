using MediatR;
using Microsoft.AspNetCore.Authorization;
using TeknikServis.WebAPI.Abstractions;

namespace TeknikServis.WebAPI.Controllers;

[AllowAnonymous]
public class UsersController : ApiController
{
    public UsersController(IMediator mediator) : base(mediator)
    {
    }
}
