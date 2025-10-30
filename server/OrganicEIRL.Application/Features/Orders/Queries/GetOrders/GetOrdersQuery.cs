using MediatR;
using OrganicEIRL.Application.DTOs;

namespace OrganicEIRL.Application.Features.Orders.Queries.GetOrders;

public class GetOrdersQuery : IRequest<PagedResult<OrderDto>>
{
  public int PageNumber { get; set; } = 1;
  public int PageSize { get; set; } = 10;
}