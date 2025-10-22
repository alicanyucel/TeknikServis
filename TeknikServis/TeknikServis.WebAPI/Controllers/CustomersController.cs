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
    [Authorize(Roles = RoleNames.Admin + "," + RoleNames.Customer)]
    public async Task<IActionResult> CreateCustomer(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "Customer created." })
            : BadRequest(new { success = false, message = "Failed to create customer.", errors = result.ErrorMessages });
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CustomerGetById(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "Customer retrieved.", data = result.Data })
            : NotFound(new { success = false, message = "Customer not found.", errors = result.ErrorMessages });
    }
    [HttpPost]
    [Authorize(Roles = RoleNames.Admin + "," + RoleNames.Customer)]
    public async Task<IActionResult> CustomerDelete(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "Customer deleted." })
            : BadRequest(new { success = false, message = "Failed to delete customer.", errors = result.ErrorMessages });
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll(GetAllCustomerQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "Customers listed.", data = result.Data })
            : BadRequest(new { success = false, message = "Failed to list customers.", errors = result.ErrorMessages });
    }

    [HttpPost]
    [Authorize(Roles = RoleNames.Admin + "," + RoleNames.Customer)]
    public async Task<IActionResult> UpdateCustomer(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "Customer updated." })
            : BadRequest(new { success = false, message = "Failed to update customer.", errors = result.ErrorMessages });
    }
}