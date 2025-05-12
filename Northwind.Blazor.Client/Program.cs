using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Northwind.EntityModels;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
// We would need this path hint if we used an additional folder containing the blazor projects. see p. 717
//builder.Services.AddNorthwindContext(relativePath: @"..\..");
builder.Services.AddNorthwindContext();

await builder.Build().RunAsync();
