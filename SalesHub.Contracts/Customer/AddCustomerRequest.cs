namespace SalesHub.Contracts.Customer
{
    public record AddCustomerRequest(
        string FirstName,
        string LastName,
        string Phone,
        string Email);
}