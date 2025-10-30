namespace OrganicEIRL.Application.DTOs;

public class OrderDto
{
  public int Id { get; set; }
  public DateTime OrderDate { get; set; }
  public int CustomerId { get; set; }
  public string CustomerName { get; set; } = string.Empty;
  public decimal TotalPrice { get; set; }
  public DateTime Created { get; set; }
  public List<OrderDetailDto> OrderDetails { get; set; } = new();
}

public class OrderDetailDto
{
  public int ProductId { get; set; }
  public string ProductDescription { get; set; } = string.Empty;
  public int Quantity { get; set; }
  public decimal UnitPrice { get; set; }
  public decimal Subtotal { get; set; }
}