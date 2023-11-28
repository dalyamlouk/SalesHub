namespace SalesHub.Domain.Entities;

public class Product 
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string SKU { get; set; } = null!;
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}    