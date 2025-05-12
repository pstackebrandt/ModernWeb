using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Northwind.EntityModels;
using Northwind.Blazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
// We would need this path hint if we used an additional folder containing the blazor projects. see p. 717
//builder.Services.AddNorthwindContext(relativePath: @"..\..");

builder.Services.AddNorthwindContext();
//builder.Services.AddTransient<INorthwindService, NorthwindServiceServerSide>();

await builder.Build().RunAsync();
