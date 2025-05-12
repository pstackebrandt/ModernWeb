using Northwind.EntityModels;

namespace Northwind.Blazor.Services;

/*
    This interface defines the methods for the Northwind service.
*/
public interface INorthwindService
{
    Task<List<Customer>> GetCustomersAsync(); // Get all customers
    Task<List<Customer>> GetCustomersAsync(string country); // Get customers by country
    Task<Customer?> GetCustomerAsync(string id); // Get a single customer by id
    Task<Customer> CreateCustomerAsync(Customer c); // Create a new customer
    Task<Customer> UpdateCustomerAsync(Customer c); // Update an existing customer
    Task DeleteCustomerAsync(string id); // Delete a customer by id
}
