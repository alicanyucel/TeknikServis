using MediatR;
using Microsoft.AspNetCore.Authorization;
using TeknikServis.WebAPI.Abstractions;

namespace TeknikServis.WebAPI.Controllers;

[AllowAnonymous]
public class ServicesController : ApiController
{
    public ServicesController(IMediator mediator) : base(mediator)
    {
    }
}
