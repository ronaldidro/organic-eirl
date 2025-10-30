using MediatR;
using Microsoft.EntityFrameworkCore;
using OrganicEIRL.Application.DTOs;
using OrganicEIRL.Application.Interfaces;
using OrganicEIRL.Domain.Common;
using OrganicEIRL.Domain.Entities;

namespace OrganicEIRL.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<OrderDto>>
{
  private readonly IApplicationDbContext _context;

  public CreateOrderCommandHandler(IApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<Result<OrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
  {
    await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

    try
    {
      var customerExists = await _context.Customers
        .AnyAsync(c => c.Id == request.CustomerId, cancellationToken);

      if (!customerExists)
        return Result<OrderDto>.Failure("Cliente no encontrado");

      var productIds = request.OrderDetails.Select(od => od.ProductId).ToList();
      var products = await _context.Products
          .Where(p => productIds.Contains(p.Id))
          .ToDictionaryAsync(p => p.Id, p => p, cancellationToken);

      var missingProducts = productIds.Except(products.Keys).ToList();
      if (missingProducts.Any())
        return Result<OrderDto>.Failure($"Productos no encontrados: {string.Join(", ", missingProducts)}");

      decimal calculatedTotal = 0;
      var calculationErrors = new List<string>();

      foreach (var item in request.OrderDetails)
      {
        var calculatedSubtotal = item.Quantity * item.UnitPrice;

        if (item.Subtotal != calculatedSubtotal)
          calculationErrors.Add($"Subtotal incorrecto para producto {products[item.ProductId].Description}. Esperado: {calculatedSubtotal}, Recibido: {item.Subtotal}");

        calculatedTotal += calculatedSubtotal;
      }

      if (request.TotalPrice != calculatedTotal)
        calculationErrors.Add($"Total incorrecto. Esperado: {calculatedTotal}, Recibido: {request.TotalPrice}");

      if (calculationErrors.Any())
        return Result<OrderDto>.Failure(calculationErrors);

      var order = new Order
      {
        OrderDate = request.OrderDate,
        CustomerId = request.CustomerId,
        TotalPrice = calculatedTotal
      };

      foreach (var item in request.OrderDetails)
      {
        var orderDetail = new OrderDetail
        {
          ProductId = item.ProductId,
          Quantity = item.Quantity,
          UnitPrice = item.UnitPrice,
          Subtotal = item.Quantity * item.UnitPrice
        };
        order.OrderDetails.Add(orderDetail);
      }

      _context.Orders.Add(order);
      await _context.SaveChangesAsync(cancellationToken);
      await transaction.CommitAsync(cancellationToken);

      var orderDto = new OrderDto
      {
        Id = order.Id,
        OrderDate = order.OrderDate,
        CustomerId = order.CustomerId,
        TotalPrice = order.TotalPrice,
        OrderDetails = order.OrderDetails.Select(od => new OrderDetailDto
        {
          ProductId = od.ProductId,
          Quantity = od.Quantity,
          UnitPrice = od.UnitPrice,
          Subtotal = od.Subtotal
        }).ToList()
      };

      return Result<OrderDto>.Success(orderDto);
    }
    catch (Exception ex)
    {
      await transaction.RollbackAsync(cancellationToken);
      return Result<OrderDto>.Failure($"Error al crear pedido: {ex.Message}");
    }
  }
}