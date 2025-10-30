using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrganicEIRL.Application.Exceptions;
using OrganicEIRL.Application.Features.Orders.Commands.CreateOrder;
using OrganicEIRL.Application.Features.Orders.Commands.DeleteOrder;
using OrganicEIRL.Application.Features.Orders.Queries.GetOrderById;
using OrganicEIRL.Application.Features.Orders.Queries.GetOrders;

namespace OrganicEIRL.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
  private readonly IMediator _mediator;

  public OrdersController(IMediator mediator)
  {
    _mediator = mediator;
  }

  [HttpGet]
  public async Task<ActionResult> GetOrders([FromQuery] GetOrdersQuery query)
  {
    var result = await _mediator.Send(query);
    return Ok(result);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult> GetOrderById(int id)
  {
    var result = await _mediator.Send(new GetOrderByIdQuery { OrderId = id });

    if (!result.IsSuccess)
      return NotFound(result.Errors);

    return Ok(result.Data);
  }

  [HttpPost]
  public async Task<ActionResult> CreateOrder(CreateOrderCommand command)
  {
    try
    {
      var result = await _mediator.Send(command);

      if (!result.IsSuccess)
        return BadRequest(new { errors = result.Errors });

      return Ok(result.Data);
    }
    catch (ValidationException ex)
    {
      return BadRequest(new { errors = ex.Errors.Values.SelectMany(v => v).ToList() });
    }
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult> DeleteOrder(int id)
  {
    try
    {
      var result = await _mediator.Send(new DeleteOrderCommand { OrderId = id });

      if (!result.IsSuccess)
        return BadRequest(new { errors = result.Errors });

      return Ok(new { message = "Orden eliminada correctamente" });
    }
    catch (ValidationException ex)
    {
      return BadRequest(new { errors = ex.Errors.Values.SelectMany(v => v).ToList() });
    }
  }
}