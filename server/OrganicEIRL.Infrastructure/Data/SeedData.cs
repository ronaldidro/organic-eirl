using Microsoft.EntityFrameworkCore;
using OrganicEIRL.Domain.Entities;

namespace OrganicEIRL.Infrastructure.Data;

public static class SeedData
{
  public static async Task SeedAsync(ApplicationDbContext context)
  {
    if (!await context.Customers.AnyAsync())
    {
      var customers = new List<Customer>
            {
                new() { Code = "CLI001", FullName = "Juan Pérez García", DNI = "12345678" },
                new() { Code = "CLI002", FullName = "María Rodríguez López", DNI = "87654321" },
                new() { Code = "CLI003", FullName = "Carlos Sánchez Martínez", DNI = "11223344" },
                new() { Code = "CLI004", FullName = "Ana Gómez Fernández", DNI = "44332211" },
                new() { Code = "CLI005", FullName = "Luis Torres Díaz", DNI = "55667788" }
            };

      await context.Customers.AddRangeAsync(customers);
      await context.SaveChangesAsync();
    }

    if (!await context.Products.AnyAsync())
    {
      var products = new List<Product>
            {
                new() { Code = "PROD001", Description = "Arroz Orgánico Bolsa 5kg" },
                new() { Code = "PROD002", Description = "Quinua Premium Bolsa 1kg" },
                new() { Code = "PROD003", Description = "Aceite de Oliva Extra Virgen 500ml" },
                new() { Code = "PROD004", Description = "Miel Natural Frasco 500g" },
                new() { Code = "PROD005", Description = "Café Orgánico Molido 250g" },
                new() { Code = "PROD006", Description = "Harina de Trigo Integral 1kg" },
                new() { Code = "PROD007", Description = "Azúcar Rubia Orgánica 1kg" },
                new() { Code = "PROD008", Description = "Leche de Almendras 1L" }
            };

      await context.Products.AddRangeAsync(products);
      await context.SaveChangesAsync();
    }
  }
}