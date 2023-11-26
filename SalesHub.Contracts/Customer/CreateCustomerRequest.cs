namespace SalesHub.Contracts.Customer
{
    public record CreateCustomerRequest(
        string FirstName,
        string LastName,
        string Phone,
        string Email);
}