using MediatR;
using OrganicEIRL.Domain.Common;

namespace OrganicEIRL.Application.Features.Orders.Commands.DeleteOrder;

public class DeleteOrderCommand : IRequest<Result<bool>>
{
  public int OrderId { get; set; }
}