using MediatR;
using OrganicEIRL.Application.DTOs;
using OrganicEIRL.Domain.Common;

namespace OrganicEIRL.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<Result<OrderDto>>
{
  public DateTime OrderDate { get; set; }
  public int CustomerId { get; set; }
  public decimal TotalPrice { get; set; }
  public List<OrderDetailItem> OrderDetails { get; set; } = new();
}

public class OrderDetailItem
{
  public int ProductId { get; set; }
  public int Quantity { get; set; }
  public decimal UnitPrice { get; set; }
  public decimal Subtotal { get; set; }
}