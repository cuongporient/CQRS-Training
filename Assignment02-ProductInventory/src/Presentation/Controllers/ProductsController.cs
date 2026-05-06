using MediatR;
using Microsoft.AspNetCore.Mvc;
using Assignment02_ProductInventory.Presentation.Models;

namespace Assignment02_ProductInventory.Presentation.Controllers;

[ApiController]
[Route("products")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // POST /products
    // Body: { "name": "Pen", "sku": "PEN-001", "stock": 50, "price": 1.99 }
    // Returns: 201 Created with the new product ID
    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] AddProductRequest request)
    {
        // TODO: Create an AddProductCommand from the request, send it via _mediator
        // Return CreatedAtAction with the new product ID
        throw new NotImplementedException();
    }

    // POST /products/{id}/restock
    // Body: { "quantity": 100 }
    // Returns: 200 OK with the updated stock count
    [HttpPost("{id:guid}/restock")]
    public async Task<IActionResult> Restock(Guid id, [FromBody] RestockRequest request)
    {
        // TODO: Create a RestockCommand from id + request, send it via _mediator
        // Return Ok(new { stock = updatedStock })
        throw new NotImplementedException();
    }

    // GET /products/low-stock?threshold=10
    // Returns: 200 OK with list of low-stock products
    [HttpGet("low-stock")]
    public async Task<IActionResult> GetLowStock([FromQuery] int threshold = 10)
    {
        // TODO: Create a GetLowStockQuery with the threshold, send it via _mediator
        // Return Ok(items)
        throw new NotImplementedException();
    }
}
