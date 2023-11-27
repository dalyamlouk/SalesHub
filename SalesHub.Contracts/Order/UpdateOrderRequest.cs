namespace SalesHub.Contracts.Order
{
    public record UpdateOrderRequest(
                            Guid Id,
                            int Status,
                            DateTime UpdatedDate);
}