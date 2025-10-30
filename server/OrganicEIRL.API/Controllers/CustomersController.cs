using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrganicEIRL.Application.DTOs;
using OrganicEIRL.Application.Features.Customers.Queries.GetCustomers;

namespace OrganicEIRL.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
  private readonly IMediator _mediator;

  public CustomersController(IMediator mediator)
  {
    _mediator = mediator;
  }

  [HttpGet]
  public async Task<ActionResult<List<CustomerDto>>> GetCustomers()
  {
    var customers = await _mediator.Send(new GetCustomersQuery());
    return Ok(customers);
  }
}