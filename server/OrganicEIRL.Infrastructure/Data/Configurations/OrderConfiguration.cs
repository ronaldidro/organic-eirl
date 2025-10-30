using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrganicEIRL.Domain.Entities;

namespace OrganicEIRL.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
  public void Configure(EntityTypeBuilder<Order> builder)
  {
    builder.HasKey(o => o.Id);

    builder.Property(o => o.OrderDate)
        .IsRequired();

    builder.Property(o => o.TotalPrice)
        .HasColumnType("decimal(18,2)")
        .IsRequired();

    builder.Property(o => o.IsActive)
       .HasDefaultValue(true);

    builder.HasOne(o => o.Customer)
        .WithMany(c => c.Orders)
        .HasForeignKey(o => o.CustomerId)
        .OnDelete(DeleteBehavior.Restrict);
  }
}