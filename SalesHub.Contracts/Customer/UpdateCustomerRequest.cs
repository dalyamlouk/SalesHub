namespace SalesHub.Contracts.Customer
{
    public record UpdateCustomerRequest(
        Guid Id,
        string FirstName,
        string LastName,
        string Phone,
        string Email);
}