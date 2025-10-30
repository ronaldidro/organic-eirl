using MediatR;
using OrganicEIRL.Application.DTOs;
using OrganicEIRL.Domain.Common;

namespace OrganicEIRL.Application.Features.Orders.Queries.GetOrderById;

public class GetOrderByIdQuery : IRequest<Result<OrderDto>>
{
  public int OrderId { get; set; }
}