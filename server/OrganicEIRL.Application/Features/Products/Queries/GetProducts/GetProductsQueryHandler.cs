using MediatR;
using Microsoft.EntityFrameworkCore;
using OrganicEIRL.Application.DTOs;
using OrganicEIRL.Application.Interfaces;

namespace OrganicEIRL.Application.Features.Products.Queries.GetProducts;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductDto>>
{
  private readonly IApplicationDbContext _context;

  public GetProductsQueryHandler(IApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
  {
    return await _context.Products
        .OrderBy(p => p.Description)
        .Select(p => new ProductDto
        {
          Id = p.Id,
          Description = p.Description
        })
        .ToListAsync(cancellationToken);
  }
}