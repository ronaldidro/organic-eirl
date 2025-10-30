using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrganicEIRL.Application.Features.Products.Queries.GetProducts;

namespace OrganicEIRL.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
  private readonly IMediator _mediator;

  public ProductsController(IMediator mediator)
  {
    _mediator = mediator;
  }

  [HttpGet]
  public async Task<ActionResult> GetProducts()
  {
    var products = await _mediator.Send(new GetProductsQuery());
    return Ok(products);
  }
}