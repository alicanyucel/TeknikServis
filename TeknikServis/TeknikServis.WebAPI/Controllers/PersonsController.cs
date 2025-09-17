using MediatR;
using Microsoft.AspNetCore.Authorization;
using TeknikServis.WebAPI.Abstractions;

namespace TeknikServis.WebAPI.Controllers;

[AllowAnonymous]
public class PersonsController : ApiController
{
    public PersonsController(IMediator mediator) : base(mediator)
    {
    }
}
