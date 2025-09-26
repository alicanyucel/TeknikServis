using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeknikServis.Application.Features.Customers.CreateCustomer;
using TeknikServis.Application.Features.Customers.CustomerGetById;
using TeknikServis.Application.Features.Customers.DeleteCustomers;
using TeknikServis.Application.Features.Customers.GetAllCustomers;
using TeknikServis.Application.Features.Customers.UpdateCustomer;
using TeknikServis.WebAPI.Abstractions;
using TeknikServis.Application.Constanst;

namespace TeknikServis.WebAPI.Controllers;

public class CustomersController : ApiController
{
    public CustomersController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<IActionResult> CreateCustomer(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return NoContent();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CustomerGetById(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);
    }
    [HttpPost]
    [Authorize(Roles = RoleNames.Admin + "," + RoleNames.Customer)]
    public async Task<IActionResult> CustomerDelete(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return NoContent();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll(GetAllCustomerQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    [Authorize(Roles = RoleNames.Admin + "," + RoleNames.Customer)]
    public async Task<IActionResult> UpdateCustomer(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}