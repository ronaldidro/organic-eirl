using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrganicEIRL.Domain.Entities;

namespace OrganicEIRL.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
  public void Configure(EntityTypeBuilder<Product> builder)
  {
    builder.HasKey(p => p.Id);

    builder.Property(p => p.Code)
        .IsRequired()
        .HasMaxLength(20);

    builder.HasIndex(p => p.Code)
        .IsUnique();

    builder.Property(p => p.Description)
        .IsRequired()
        .HasMaxLength(200);
  }
}