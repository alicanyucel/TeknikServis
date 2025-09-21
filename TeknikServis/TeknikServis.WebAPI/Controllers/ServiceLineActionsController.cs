using MediatR;
using Microsoft.AspNetCore.Authorization;
using TeknikServis.WebAPI.Abstractions;

namespace TeknikServis.WebAPI.Controllers;
[AllowAnonymous]
public class ServiceLineActionsController : ApiController
{
    public ServiceLineActionsController(IMediator mediator) : base(mediator)
    {
    }
}
