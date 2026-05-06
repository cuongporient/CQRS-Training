using MediatR;
using Microsoft.AspNetCore.Mvc;
using Assignment01_OrderManagement.Presentation.Models;

namespace Assignment01_OrderManagement.Presentation.Controllers;

[ApiController]
[Route("orders")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // POST /orders
    // Body: { "customerName": "Alice", "items": [{ "productName": "Book", "quantity": 2, "price": 15.99 }] }
    // Returns: 201 Created with the new order ID
    [HttpPost]
    public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderRequest request)
    {
        // TODO: Create a PlaceOrderCommand from the request, send it via _mediator
        // Return CreatedAtAction with the new order ID
        throw new NotImplementedException();
    }

    // GET /orders/{id}
    // Returns: 200 OK with order data, or 404 NotFound
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOrder(Guid id)
    {
        // TODO: Create a GetOrderByIdQuery, send it via _mediator
        // Return Ok(order) if found, NotFound() if null
        throw new NotImplementedException();
    }

    // DELETE /orders/{id}
    // Returns: 204 NoContent if cancelled, 400 BadRequest if already cancelled
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> CancelOrder(Guid id)
    {
        // TODO: Create a CancelOrderCommand, send it via _mediator
        // Return NoContent() on success, BadRequest() if already cancelled
        throw new NotImplementedException();
    }
}
