using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using OrganicEIRL.Domain.Entities;

namespace OrganicEIRL.Application.Interfaces;

public interface IApplicationDbContext
{
  DbSet<Customer> Customers { get; }
  DbSet<Product> Products { get; }
  DbSet<Order> Orders { get; }
  DbSet<OrderDetail> OrderDetails { get; }

  DatabaseFacade Database { get; }

  Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}