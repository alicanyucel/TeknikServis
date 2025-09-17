using MediatR;
using Microsoft.AspNetCore.Authorization;
using TeknikServis.WebAPI.Abstractions;

namespace TeknikServis.WebAPI.Controllers;

[AllowAnonymous]
public class ProductsController : ApiController
{
    public ProductsController(IMediator mediator) : base(mediator)
    {
    }
}
