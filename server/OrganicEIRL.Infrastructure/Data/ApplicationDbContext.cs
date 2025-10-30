using Microsoft.EntityFrameworkCore;
using OrganicEIRL.Application.Interfaces;
using OrganicEIRL.Domain.Entities;
using System.Reflection;

namespace OrganicEIRL.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

  public DbSet<Customer> Customers => Set<Customer>();
  public DbSet<Product> Products => Set<Product>();
  public DbSet<Order> Orders => Set<Order>();
  public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();

  protected override void OnModelCreating(ModelBuilder builder)
  {
    builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    base.OnModelCreating(builder);
  }

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
  {

    foreach (var entry in ChangeTracker.Entries<Order>())
    {
      if (entry.State == EntityState.Added)
      {
        entry.Entity.Created = DateTime.UtcNow;
      }
      else if (entry.State == EntityState.Modified)
      {
        entry.Entity.LastModified = DateTime.UtcNow;
      }
    }

    return await base.SaveChangesAsync(cancellationToken);
  }
}