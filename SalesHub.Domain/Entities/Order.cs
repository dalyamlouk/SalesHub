namespace SalesHub.Domain.Entities;

public class Order 
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ProductId { get; set; } = Guid.NewGuid();
    public Guid CustomerId { get; set; } = Guid.NewGuid();
    public int Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}