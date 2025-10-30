namespace OrganicEIRL.Domain.Entities;

public class Customer
{
  public int Id { get; set; }
  public string Code { get; set; } = string.Empty;
  public string FullName { get; set; } = string.Empty;
  public string DNI { get; set; } = string.Empty;

  public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}