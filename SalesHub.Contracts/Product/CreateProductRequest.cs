namespace SalesHub.Contracts.Product
{
    public record CreateProductRequest(
        string Name,
        string Description,
        string SKU);
}