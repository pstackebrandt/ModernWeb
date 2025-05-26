using Microsoft.AspNetCore.Mvc; // To use ProblemDetails
using Northwind.EntityModels; // for Customer
using Northwind.WebApi.Repositories; // for ICustomerRepository

namespace Northwind.WebApi;

/// <summary>
/// Customer-related endpoints.
/// </summary>
static partial class Program
{
    /// <summary>
    /// Map customer-related endpoints.
    /// </summary>
    /// <param name="app">The web application.</param>
    internal static void MapCustomers(this WebApplication app)
    {
        // Retrieve all customers
        app.MapGet(
            pattern: "/customers",
            handler: async (ICustomerRepository repo) =>
            {
                return await repo.RetrieveAllAsync();
            });

        // Retrieve all customer of country
        app.MapGet(
            pattern: "/customers/in/{country}",
            handler:
                async (string country, ICustomerRepository repo) =>
                {
                    // Filter customers by country
                    // Current implementation is inefficient for large datasets, because it retrieves all customers and after that filters them. Possibly used by Price to simplify the code.
                    // TODO: Implement a repository method that returns a filtered list of customers without retrieving all customers first.
                    return (await repo.RetrieveAllAsync())
                                .Where(customer => customer.Country == country);
                }
            );
    }
}


