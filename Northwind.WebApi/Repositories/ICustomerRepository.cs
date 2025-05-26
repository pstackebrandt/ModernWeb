using Northwind.EntityModels; // to use Customer

namespace Northwind.WebApi.Repositories;

/// <summary>
/// Interface for Customer repository
/// </summary>
public interface ICustomerRepository
{
    Task<Customer?> CreateAsync(Customer c);
    
    Task<Customer[]> RetrieveAllAsync();

    /// <summary>
    /// Retrieve a customer by customer id
    /// </summary>
    /// <param name="id">The customer id</param>
    /// <param name="token">The cancellation token</param>
    /// <returns>The customer</returns>
    Task<Customer?> RetrieveAsync(string id, CancellationToken token);

    /// <summary>
    /// Update a customer
    /// </summary>
    /// <param name="c">The customer</param>
    /// <returns>The updated customer</returns>
    Task<Customer?> UpdateAsync(Customer c);

    /// <summary>
    /// Delete a customer by customer id
    /// </summary>
    /// <param name="id">The id of the customer</param>
    /// <returns>True if the customer was deleted, false otherwise</returns>
    Task<bool?> DeleteAsync(string id);
}
