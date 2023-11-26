namespace SalesHub.Applications.Customer.Common
{
    public record UpdateCustomerResult(
        Guid Id,
        string FirstName,
        string LastName,
        string Phone,
        string Email);
}