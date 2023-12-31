namespace SalesHub.Application.Customer.Common
{
    public record CreateCustomerResult(
        Guid Id,
        string FirstName,
        string LastName,
        string Phone,
        string Email);
}