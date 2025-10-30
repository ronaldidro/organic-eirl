using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrganicEIRL.Domain.Entities;

namespace OrganicEIRL.Infrastructure.Data.Configurations;

public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
  public void Configure(EntityTypeBuilder<OrderDetail> builder)
  {
    builder.HasKey(od => od.Id);

    builder.Property(od => od.Quantity)
        .IsRequired()
        .HasDefaultValue(1);

    builder.Property(od => od.UnitPrice)
        .HasColumnType("decimal(18,2)")
        .IsRequired();

    builder.Property(od => od.Subtotal)
        .HasColumnType("decimal(18,2)")
        .IsRequired();

    builder.ToTable(tb => tb.HasCheckConstraint("CK_OrderDetail_Quantity", "[Quantity] > 0"));
    builder.ToTable(tb => tb.HasCheckConstraint("CK_OrderDetail_UnitPrice", "[UnitPrice] > 0"));

    builder.HasOne(od => od.Order)
        .WithMany(o => o.OrderDetails)
        .HasForeignKey(od => od.OrderId)
        .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(od => od.Product)
        .WithMany(p => p.OrderDetails)
        .HasForeignKey(od => od.ProductId)
        .OnDelete(DeleteBehavior.Restrict);
  }
}