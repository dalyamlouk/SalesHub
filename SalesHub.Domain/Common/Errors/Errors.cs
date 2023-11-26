using ErrorOr;

namespace SalesHub.Domain.Common.Errors;

public static class Errors 
{
    public static class Customer
    {
        public static Error AlreadyExists(string email) =>
            Error.Conflict(code: "Customer.AlreadyExists",
                           description: $"Customer with email {email} already exists.");

        public static Error NotFound(string email) =>
            Error.Conflict(code: "Customer.NotFound",
                           description: $"Customer with email {email} could not be found.");

        public static Error NotFound(Guid id) =>
            Error.Conflict(code: "Customer.NotFound",
                           description: $"Customer with Id {id} could not be found.");
    }

    public static class Product
    {
        public static Error AlreadyExists(string sku) =>
            Error.Conflict(code: "Product.AlreadyExists",
                           description: $"Product with SKU {sku} already exists.");

        public static Error NotFound(string sku) =>
            Error.Conflict(code: "Product.NotFound",
                           description: $"Product with SKU {sku} could not be found.");

        public static Error NotFound(Guid id) =>
            Error.Conflict(code: "Product.NotFound",
                           description: $"Product with Id {id} could not be found.");
    }
}