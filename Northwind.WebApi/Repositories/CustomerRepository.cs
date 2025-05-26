using Microsoft.EntityFrameworkCore.ChangeTracking; // To use EntityEntry<T>
using Northwind.EntityModels; // to use Customer
using Microsoft.EntityFrameworkCore; // To use ToArrayAsync
using Microsoft.Extensions.Caching.Hybrid; // for HybridCache

namespace Northwind.WebApi.Repositories;

/// <summary>
/// Repository for Customer entity
/// Reads and writes a hybrid cache for performance.
/// </summary>
public class CustomerRepository : ICustomerRepository
{
    private readonly HybridCache _cache;

    // Use an instance data context field because it should not be
    // cached due to the data context having internal caching.
    private NorthwindContext _db;

    public CustomerRepository(NorthwindContext db, HybridCache hybridCache)
    {
        _db = db;
        _cache = hybridCache;
    }

    public Task<Customer[]> RetrieveAllAsync()
    {
        return _db.Customers.ToArrayAsync();
    }

    /// <summary>
    /// Retrieve a customer by customer id. Use cached data if available or 
    /// get from database and update cache.
    /// </summary>
    /// <param name="id">The customer id</param>
    /// <param name="token">The cancellation token</param>
    /// <returns>The customer</returns>
    public async Task<Customer?> RetrieveAsync(string id,
        CancellationToken token = default)
    {
        id = id.ToUpper();

        return await _cache.GetOrCreateAsync(
            key: id, // Unique key to the cache entry.
            factory: async cancel => await _db.Customers.FirstOrDefaultAsync(
                c => c.CustomerId == id,
                token),
                cancellationToken: token);
    }

    /// <summary>
    /// Create a customer
    /// </summary>
    /// <param name="c">The customer</param>
    /// <returns>The created customer</returns>
    public async Task<Customer?> CreateAsync(Customer c)
    {
        c.CustomerId = c.CustomerId.ToUpper(); // Normalize to uppercase

        // Add to database using EF Core
        // TODO: Use the returned customer. 
        EntityEntry<Customer> added = await _db.Customers.AddAsync(c);

        int affected = await _db.SaveChangesAsync();
        if (affected == 1)
        {
            // If saved to database then store in cache
            await _cache.SetAsync(c.CustomerId, c);
            return c;
        }
        return null;
    }

    public async Task<Customer?> UpdateAsync(Customer c)
    {
        c.CustomerId = c.CustomerId.ToUpper(); // Normalize to uppercase

        _db.Customers.Update(c);

        int affected = await _db.SaveChangesAsync();
        if (affected == 1)
        {
            // If saved to database then store in cache
            await _cache.SetAsync(c.CustomerId, c);
            return c;
        }

        return null;
    }


    /// <summary>
    /// Delete a customer identified by its id.
    /// </summary>
    /// <param name="id">Id of the Customer to be deleted.</param>
    /// <returns>true if Customer now deleted, null: Customer not found or deletion failed or unexpected behavior on deletion</returns>
    public async Task<bool?> DeleteAsync(string id)
    {
        // Implemented return value and checks by Price could be improved to check input and document failure cases specifically.

        id = id.ToUpper(); // Normalize to uppercase

        // Find the customer in the database
        Customer? customerInDb = await _db.Customers.FindAsync(id);
        if (customerInDb is null) return null; // Customer not found

        // Remove the customer from the database
        _db.Customers.Remove(customerInDb);

        int affected = await _db.SaveChangesAsync();

        if (affected == 1)
        {
            // If saved to database then remove from cache
            await _cache.RemoveAsync(customerInDb.CustomerId); // Price never checked, whether the 
            return true;
        }

        return null; // Price returns null.
    }
}


        