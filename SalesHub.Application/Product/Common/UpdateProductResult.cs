namespace SalesHub.Applications.Product.Common
{
    public record UpdateProductResult(
        Guid Id,
        string Name,
        string Description,
        string SKU);
}