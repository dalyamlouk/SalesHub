namespace SalesHub.Application.Product.Common
{
    public record CreateProductResult(
        Guid Id,
        string Name,
        string Description,
        string SKU);
}