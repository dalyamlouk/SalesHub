namespace SalesHub.Contracts.Product
{
    public record UpdateProductRequest(
        Guid Id,
        string Name,
        string Description,
        string SKU);
}