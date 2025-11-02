using MediatR;
using Microsoft.AspNetCore.Authorization;
using TeknikServis.WebAPI.Abstractions;

namespace TeknikServis.WebAPI.Controllers;

[AllowAnonymous]
public class ProvincesController : ApiController
{
    public ProvincesController(IMediator mediator) : base(mediator)
    {
    }
}
