using Mediator.Net;
using Microsoft.AspNetCore.Mvc;
using Assignment02_ProductInventory.Presentation.Models;
using Assignment02_ProductInventory.Application.Commands;
using Assignment02_ProductInventory.Application.Queries;
using Assignment02_ProductInventory.Application.DTOs;

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
        var command = new AddProductCommand(request.Name, request.Sku, request.Stock, request.Price);
        var response = await _mediator.RequestAsync<AddProductCommand, Assignment02_ProductInventory.Application.Responses.AddProductResponse>(command);
        return CreatedAtAction(nameof(AddProduct), new { id = response.Id }, new { id = response.Id });
    }

    // POST /products/{id}/restock
    // Body: { "quantity": 100 }
    // Returns: 200 OK with the updated stock count
    [HttpPost("{id:guid}/restock")]
    public async Task<IActionResult> Restock(Guid id, [FromBody] RestockRequest request)
    {
        var command = new RestockCommand(id, request.Quantity);
        var response = await _mediator.RequestAsync<RestockCommand, Assignment02_ProductInventory.Application.Responses.RestockResponse>(command);
        return Ok(new { stock = response.Stock });
    }

    // GET /products/low-stock?threshold=10
    // Returns: 200 OK with list of low-stock products
    [HttpGet("low-stock")]
    public async Task<IActionResult> GetLowStock([FromQuery] int threshold = 10)
    {
        var query = new GetLowStockQuery(threshold);
        var response = await _mediator.RequestAsync<GetLowStockQuery, Assignment02_ProductInventory.Application.Responses.GetLowStockResponse>(query);
        return Ok(response.Items);
    }
}
