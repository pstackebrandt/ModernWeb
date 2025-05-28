using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Northwind.WebAPI.WasmClient;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure the HTTP client
builder.Services.AddScoped(sp => 
    new HttpClient
    {
        // Server address of the Northwind Web API
        BaseAddress = new Uri("https://localhost:5151/")
    }
);
