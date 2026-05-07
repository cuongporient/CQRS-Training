using MediatR;
using Microsoft.AspNetCore.Mvc;
using Assignment01_OrderManagement.Presentation.Models;
using Assignment01_OrderManagement.Application.Commands;
using Assignment01_OrderManagement.Application.Queries;

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
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderRequest request, CancellationToken cancellationToken)
    {
        // TODO: Create a PlaceOrderCommand from the request, send it via _mediator
        // Return CreatedAtAction with the new order ID
        var commandItems = request.Items.Select(item =>
            new OrderItemRequestDto(item.ProductName, item.Quantity, item.Price)
        ).ToList();

        var orderCreatedId = await _mediator.Send(new PlaceOrderCommand(request.CustomerName, commandItems));
        
        return CreatedAtAction(nameof(GetOrder), new { id = orderCreatedId }, orderCreatedId);
    }

    // GET /orders/{id}
    // Returns: 200 OK with order data, or 404 NotFound
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrder(Guid id)
    {
        var orderGetResult = await _mediator.Send(new GetOrderByIdQuery(id));
        
        return orderGetResult is not null ? Ok(orderGetResult) : NotFound();
    }

    // DELETE /orders/{id}
    // Returns: 204 NoContent if cancelled, 400 BadRequest if already cancelled
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CancelOrder(Guid id)
    {
        // TODO: Create a CancelOrderCommand, send it via _mediator
        // Return NoContent() on success, BadRequest() if already cancelled
        var result = await _mediator.Send(new CancelOrderCommand(id));

        return result ? NoContent() : BadRequest();
    }
}
