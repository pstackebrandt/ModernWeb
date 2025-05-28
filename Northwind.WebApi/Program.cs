using Northwind.EntityModels; // for AddNorthwindContext method
using Microsoft.Extensions.Caching.Hybrid; // for HybridCache
using Northwind.WebApi.Repositories; // for ICustomerRepository
using Microsoft.AspNetCore.HttpLogging; // for HttpLoggingFields

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// =====================================================================
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(documentName: "v2");
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

// Add HTTP logging
builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.All;
    options.RequestBodyLogLimit = 4096; // Default is 32K
    options.ResponseBodyLogLimit = 4096; // Default is 32K
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // Add openapi endpoint
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v2.json", "Northwind API v2");
    });
}

app.UseHttpLogging();

app.UseHttpsRedirection();

// Code to manage weather forecast moved to Program.Weather.cs
app.MapGet("/weatherforecast/{days:int?}",
    (int? days) => GetWeather(days ?? 5))
    .WithName("GetWeatherForecast");

// Map customer endpoints
app.MapCustomers();

app.Run();