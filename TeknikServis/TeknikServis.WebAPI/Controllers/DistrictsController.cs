using MediatR;
using Microsoft.AspNetCore.Authorization;
using TeknikServis.WebAPI.Abstractions;

namespace TeknikServis.WebAPI.Controllers;

[AllowAnonymous]
public class DistrictsController : ApiController
{
    public DistrictsController(IMediator mediator) : base(mediator)
    {
    }
}
