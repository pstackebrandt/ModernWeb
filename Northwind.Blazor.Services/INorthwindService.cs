using Northwind.EntityModels;

namespace Northwind.Blazor.Services;

/// <summary>
/// Defines the contract for Northwind service operations.
/// Defines CRUD operations for the Customer entity.
/// </summary>
public interface INorthwindService
{
    /// <summary>
    /// Get all customers.
    /// </summary>
    /// <returns>A list of all customers.</returns>
    Task<List<Customer>> GetCustomersAsync();

    /// <summary>
    /// Get all customers of a specific country.
    /// </summary>
    /// <param name="country">The country to filter customers by.</param>
    /// <returns>A list of customers from the specified country.</returns>
    Task<List<Customer>> GetCustomersAsync(string country);

    /// <summary>
    /// Get a single customer by its id.
    /// </summary>
    /// <param name="id">The id of the customer to get.</param>
    /// <returns>The customer with the specified id.</returns>
    Task<Customer?> GetCustomerAsync(string id);

    /// <summary>
    /// Create a new customer.
    /// </summary>
    /// <param name="c">Defines the customer to create.</param>
    /// <returns>The created customer.</returns>
    Task<Customer> CreateCustomerAsync(Customer c);

    /// <summary>
    /// Update an existing customer.
    /// </summary>
    /// <param name="c">Defines the customer to update.</param>
    /// <returns>The updated customer.</returns>
    Task<Customer> UpdateCustomerAsync(Customer c);

    /// <summary>
    /// Delete a customer identified by its id.
    /// </summary>
    /// <param name="id">The id of the customer to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DeleteCustomerAsync(string id);

    /// <summary>
    /// Get all countries.
    /// </summary>
    /// <returns>A list of all countries.</returns>
    /// <remarks>
    /// todo Why is this method not asynchronous?
    /// </remarks>
    List<string?> GetCountries();
}
