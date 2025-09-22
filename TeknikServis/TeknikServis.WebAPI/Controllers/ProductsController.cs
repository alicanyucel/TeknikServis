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
public class ProductsController : ApiController
{
    public ProductsController(IMediator mediator) : base(mediator)
    {
    }
    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return NoContent();
    }


    [HttpPost]
    public async Task<IActionResult> ProductGetById(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);


    }
    [HttpPost]
    public async Task<IActionResult> ProductDelete(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> GetAllProduct(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
    [HttpPost]
    public async Task<IActionResult> UpdateProduct(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}
