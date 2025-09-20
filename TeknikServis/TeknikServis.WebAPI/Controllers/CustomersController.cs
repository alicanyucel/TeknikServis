using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeknikServis.Application.Features.Customers.CreateCustomers;
using TeknikServis.Application.Features.Customers.CustomerGetById;
using TeknikServis.Application.Features.Customers.DeleteCustomers;
using TeknikServis.Application.Features.Customers.GetAllCustomers;
using TeknikServis.Application.Features.Customers.UpdateCustomers;
using TeknikServis.WebAPI.Abstractions;

namespace TeknikServis.WebAPI.Controllers;

[AllowAnonymous]
public class CustomersController : ApiController
{
    public CustomersController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateCustomer(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);

        return NoContent();
    }
    [HttpPost]
    public async Task<IActionResult> CustomerGetById(GetCustomerByIdQuery request,CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request,cancellationToken);
        return Ok(result);


    }
    [HttpPost]
    public async Task<IActionResult> CutomerDelete(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> GetAll(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}