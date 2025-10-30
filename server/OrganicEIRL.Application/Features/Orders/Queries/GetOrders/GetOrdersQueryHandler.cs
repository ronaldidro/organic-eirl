using MediatR;
using Microsoft.EntityFrameworkCore;
using OrganicEIRL.Application.DTOs;
using OrganicEIRL.Application.Interfaces;

namespace OrganicEIRL.Application.Features.Orders.Queries.GetOrders;

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, PagedResult<OrderDto>>
{
  private readonly IApplicationDbContext _context;

  public GetOrdersQueryHandler(IApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<PagedResult<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
  {
    var baseQuery = _context.Orders
        .Where(o => o.IsActive)
        .Include(o => o.Customer)
        .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
        .OrderByDescending(o => o.Id);

    var totalCount = await baseQuery.CountAsync(cancellationToken);

    var orders = await baseQuery
        .Skip((request.PageNumber - 1) * request.PageSize)
        .Take(request.PageSize)
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
        .ToListAsync(cancellationToken);

    return new PagedResult<OrderDto>
    {
      Items = orders,
      TotalCount = totalCount,
      PageNumber = request.PageNumber,
      PageSize = request.PageSize
    };
  }
}