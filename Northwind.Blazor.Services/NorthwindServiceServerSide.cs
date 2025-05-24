using Microsoft.EntityFrameworkCore;
using Northwind.EntityModels;

namespace Northwind.Blazor.Services;

/// <summary>
/// Implements CRUD operations for the Customer entity.
/// </summary>
public class NorthwindServiceServerSide : INorthwindService
{
    private readonly NorthwindContext _db;

    /// <summary>
    /// Initialize the service and add the database context.
    /// </summary>
    /// <param name="db">The database context instance.</param>
    public NorthwindServiceServerSide(NorthwindContext db)
    {
        _db = db;
    }

    public Task<List<Customer>> GetCustomersAsync()
    {
        return _db.Customers.ToListAsync();
    }

    public Task<List<Customer>> GetCustomersAsync(string country)
    {
        return _db.Customers
            .Where(c => c.Country == country)
            .ToListAsync();
    }

    public Task<Customer?> GetCustomerAsync(string id)
    {
        return _db.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);
    }

    public Task<Customer> CreateCustomerAsync(Customer c)
    {
        _db.Customers.Add(c);
        _db.SaveChangesAsync();
        return Task.FromResult(c);
    }

    public Task<Customer> UpdateCustomerAsync(Customer c)
    {
        _db.Entry(c).State = EntityState.Modified;
        _db.SaveChangesAsync();
        return Task.FromResult(c);
    }

    /// <summary>
    /// Delete a customer identified by its id.
    /// Will return without error if the customer is not found.
    /// </summary>
    /// <param name="id">The id of the customer to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task DeleteCustomerAsync(string id)
    {
        Customer? customer = _db.Customers
            .FirstOrDefaultAsync(c => c.CustomerId == id)
            .Result;

        if (customer == null)
        {
            return Task.CompletedTask;
        }
        else
        {
            _db.Customers.Remove(customer);
            return _db.SaveChangesAsync();
        }
    }

    public List<string?> GetCountries()
    {
        return _db.Customers
            .Select(c => c.Country) // Select the country column, get all country names
            .Distinct() // Get unique country names
            .OrderBy(country => country) // Order the country names alphabetically
            .ToList();
    }
}
