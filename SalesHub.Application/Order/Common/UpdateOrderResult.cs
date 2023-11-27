namespace SalesHub.Applications.Order.Common
{
    public record UpdateOrderResult(
        Guid Id,
        Guid ProductId,
        Guid CustomerId,
        int Status,
        DateTime CreatedDate,
        DateTime UpdatedDate);
}