using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrganicEIRL.Domain.Entities;

namespace OrganicEIRL.Infrastructure.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
  public void Configure(EntityTypeBuilder<Customer> builder)
  {
    builder.Property(c => c.Code)
        .IsRequired()
        .HasMaxLength(20);

    builder.HasIndex(c => c.Code)
        .IsUnique();

    builder.Property(c => c.FullName)
        .IsRequired()
        .HasMaxLength(100);

    builder.Property(c => c.DNI)
        .IsRequired()
        .HasMaxLength(15);

    builder.HasIndex(c => c.DNI)
        .IsUnique();
  }
}