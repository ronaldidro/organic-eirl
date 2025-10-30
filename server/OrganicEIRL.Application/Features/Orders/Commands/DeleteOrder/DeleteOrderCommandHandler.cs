using MediatR;
using Microsoft.EntityFrameworkCore;
using OrganicEIRL.Application.Interfaces;
using OrganicEIRL.Domain.Common;

namespace OrganicEIRL.Application.Features.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Result<bool>>
{
  private readonly IApplicationDbContext _context;

  public DeleteOrderCommandHandler(IApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<Result<bool>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
  {
    var order = await _context.Orders
        .FirstOrDefaultAsync(o => o.Id == request.OrderId && o.IsActive, cancellationToken);

    if (order == null)
      return Result<bool>.Failure("Orden no encontrada o ya fue eliminada");

    order.IsActive = false;
    order.LastModified = DateTime.UtcNow;

    await _context.SaveChangesAsync(cancellationToken);

    return Result<bool>.Success(true);
  }
}