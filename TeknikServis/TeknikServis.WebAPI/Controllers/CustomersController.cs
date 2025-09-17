using MediatR;
using Microsoft.AspNetCore.Authorization;
using TeknikServis.WebAPI.Abstractions;

namespace TeknikServis.WebAPI.Controllers;

[AllowAnonymous]
public class CustomersController : ApiController
{
    public CustomersController(IMediator mediator) : base(mediator)
    {
    }
}
