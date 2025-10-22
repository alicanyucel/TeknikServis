using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeknikServis.Application.Features.Products.CreateProduct;
using TeknikServis.Application.Features.Products.DeleteProduct;
using TeknikServis.Application.Features.Products.GetAllProduct;
using TeknikServis.Application.Features.Products.GetByIdProduct;
using TeknikServis.Application.Features.Products.UpdateProduct;
using TeknikServis.WebAPI.Abstractions;

namespace TeknikServis.WebAPI.Controllers;

[AllowAnonymous]
[Produces("application/json")]
public class ProductsController : ApiController
{
    public ProductsController(IMediator mediator) : base(mediator)
    {
    }
    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "Product created." })
            : BadRequest(new { success = false, message = "Failed to create product.", errors = result.ErrorMessages });
    }


    [HttpPost]
    public async Task<IActionResult> ProductGetById(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "Product retrieved.", data = result.Data })
            : NotFound(new { success = false, message = "Product not found.", errors = result.ErrorMessages });


    }
    [HttpPost]
    public async Task<IActionResult> ProductDelete(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccessful
            ? Ok(new { success = true, message = "Product deleted." })
            : BadRequest(new { success = false, message = "Failed to delete product.", errors = result.ErrorMessages });
    }

    [HttpPost]
    public async Task<IActionResult> GetAllProduct(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "Products listed.", data = result.Data })
            : BadRequest(new { success = false, message = "Failed to list products.", errors = result.ErrorMessages });
    }
    [HttpPost]
    public async Task<IActionResult> UpdateProduct(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccessful
            ? Ok(new { success = true, message = "Product updated." })
            : BadRequest(new { success = false, message = "Failed to update product.", errors = result.ErrorMessages });
    }
}