namespace OrganicEIRL.Domain.Entities;

public class Product
{
  public int Id { get; set; }
  public string Code { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;

  public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}