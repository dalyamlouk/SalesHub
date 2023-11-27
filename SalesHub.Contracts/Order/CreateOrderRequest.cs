namespace SalesHub.Contracts.Order
{
    public record CreateOrderRequest(
        Guid ProductId,
        Guid CustomerId,
        int Status);
}