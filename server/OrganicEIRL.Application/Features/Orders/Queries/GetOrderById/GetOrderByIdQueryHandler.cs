using MediatR;
using Microsoft.EntityFrameworkCore;
using OrganicEIRL.Application.DTOs;
using OrganicEIRL.Application.Interfaces;
using OrganicEIRL.Domain.Common;

namespace OrganicEIRL.Application.Features.Orders.Queries.GetOrderById;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Result<OrderDto>>
{
  private readonly IApplicationDbContext _context;

  public GetOrderByIdQueryHandler(IApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<Result<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
  {
    var order = await _context.Orders
        .Where(o => o.IsActive && o.Id == request.OrderId)
        .Include(o => o.Customer)
        .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
        .Select(o => new OrderDto
        {
          Id = o.Id,
          OrderDate = o.OrderDate,
          CustomerId = o.CustomerId,
          CustomerName = o.Customer.FullName,
          TotalPrice = o.TotalPrice,
          Created = o.Created,
          OrderDetails = o.OrderDetails.Select(od => new OrderDetailDto
          {
            ProductId = od.ProductId,
            ProductDescription = od.Product.Description,
            Quantity = od.Quantity,
            UnitPrice = od.UnitPrice,
            Subtotal = od.Subtotal
          }).ToList()
        })
        .FirstOrDefaultAsync(cancellationToken);

    if (order == null)
      return Result<OrderDto>.Failure("Orden no encontrada");

    return Result<OrderDto>.Success(order);
  }
}