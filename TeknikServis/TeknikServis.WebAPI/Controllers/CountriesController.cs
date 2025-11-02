using MediatR;
using Microsoft.AspNetCore.Authorization;
using TeknikServis.WebAPI.Abstractions;

namespace TeknikServis.WebAPI.Controllers;

[AllowAnonymous]
public class CountriesController : ApiController
{
    public CountriesController(IMediator mediator) : base(mediator)
    {
    }
}
