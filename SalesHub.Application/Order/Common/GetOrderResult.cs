namespace SalesHub.Application.Order.Common
{
    public record GetOrderResult(
        Guid Id,
        Guid ProductId,
        Guid CustomerId,
        int Status,
        DateTime CreatedDate,
        DateTime UpdatedDate);
}