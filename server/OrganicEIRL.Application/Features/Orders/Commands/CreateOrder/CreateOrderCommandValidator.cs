using FluentValidation;
using OrganicEIRL.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
  public CreateOrderCommandValidator()
  {
    RuleFor(x => x.CustomerId)
        .GreaterThan(0).WithMessage("Cliente es requerido");

    RuleFor(x => x.OrderDate)
        .NotEmpty().WithMessage("Fecha de pedido es requerida");

    RuleFor(x => x.TotalPrice)
        .GreaterThan(0).WithMessage("Total debe ser mayor a 0");

    RuleFor(x => x.OrderDetails)
        .NotEmpty().WithMessage("Debe agregar al menos un producto");

    RuleForEach(x => x.OrderDetails).ChildRules(detail =>
    {
      detail.RuleFor(x => x.ProductId)
          .GreaterThan(0).WithMessage("Producto es requerido");

      detail.RuleFor(x => x.Quantity)
          .GreaterThan(0).WithMessage("Cantidad debe ser mayor a 0");

      detail.RuleFor(x => x.UnitPrice)
          .GreaterThan(0).WithMessage("Precio unitario debe ser mayor a 0");

      detail.RuleFor(x => x.Subtotal)
          .GreaterThan(0).WithMessage("Subtotal debe ser mayor a 0");
    });
  }
}