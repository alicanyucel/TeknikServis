using MediatR;
using Microsoft.AspNetCore.Authorization;
using TeknikServis.WebAPI.Abstractions;

namespace TeknikServis.WebAPI.Controllers;

[AllowAnonymous]
public class CDNPControllers : ApiController
{
    public CDNPControllers(IMediator mediator) : base(mediator)
    {
    }
}
