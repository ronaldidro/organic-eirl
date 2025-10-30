namespace OrganicEIRL.Domain.Entities;

public class Order
{
  public int Id { get; set; }
  public DateTime OrderDate { get; set; }
  public int CustomerId { get; set; }
  public decimal TotalPrice { get; set; }

  public bool IsActive { get; set; } = true;
  public DateTime Created { get; set; } = DateTime.UtcNow;
  public DateTime? LastModified { get; set; }

  public virtual Customer Customer { get; set; } = null!;
  public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}