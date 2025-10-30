using MediatR;
using Microsoft.EntityFrameworkCore;
using OrganicEIRL.Application.DTOs;
using OrganicEIRL.Application.Interfaces;

namespace OrganicEIRL.Application.Features.Customers.Queries.GetCustomers;

public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, List<CustomerDto>>
{
  private readonly IApplicationDbContext _context;

  public GetCustomersQueryHandler(IApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<List<CustomerDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
  {
    var customers = await _context.Customers
        .Select(c => new CustomerDto
        {
          Id = c.Id,
          FullName = $"{c.Code} - {c.FullName}"
        })
        .ToListAsync(cancellationToken);

    return customers;
  }
}