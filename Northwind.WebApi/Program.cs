using Northwind.EntityModels; // for AddNorthwindContext method
using Microsoft.Extensions.Caching.Hybrid; // for HybridCache
using Northwind.WebApi.Repositories; // for ICustomerRepository

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddNorthwindContext();

// Configure caching, see p. 750
builder.Services.AddHybridCache(options =>
{
    options.DefaultEntryOptions = new HybridCacheEntryOptions
    {
        Expiration = TimeSpan.FromSeconds(60),
        LocalCacheExpiration = TimeSpan.FromSeconds(30)
    };
});

// Add CustomerRepository to the container
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // Add openapi endpoint
}

app.UseHttpsRedirection();

// Code to manage weather forecast moved to Program.Weather.cs

app.MapGet("/weatherforecast/{days:int?}",
    (int? days) => GetWeather(days ?? 5))
    .WithName("GetWeatherForecast");
    
app.Run();