namespace SalesHub.Application.Services.Product.Common
{
    public record GetProductResult(
        Guid Id,
        string Name,
        string Description,
        string SKU);
}