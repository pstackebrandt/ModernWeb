using Northwind.Web.Components; // To use App.

#region Configure the web server host and services.

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents(); // Alsoequired for UseHttpsRedirection

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseHsts();
}

// Enable HTTPS redirection
app.UseHttpsRedirection();

app.UseAntiforgery();

// Configure static files with proper order
app.UseDefaultFiles(); // index.html, default.html, etc.
app.UseStaticFiles(); // Required even with MapStaticAssets for proper static file handling
app.MapStaticAssets(); // .NET 9 feature for compression (has a dependency on Razor components)

app.MapRazorComponents<App>();

#endregion

#region Configure the routes and endpoints.
app.MapGet("/env", () => $"Environment: {app.Environment.EnvironmentName}");

app.MapGet("/data", () => Results.Json(new { 
    firstName = "John",
    lastName = "Doe",
    age = 30
}));

app.MapGet("/welcome", () => Results.Content(
    content: $"""
    <!DOCTYPE html>
    <html lang="en">
        <head>
            <meta charset="UTF-8">
            <meta name="viewport" content="width=device-width, initial-scale=1.0">
            <title>Welcome to the Northwind Web!</title>
        </head>
        <body>
            <h1>Welcome to the Northwind Web!</h1>
            <p>This is a simple web application built with ASP.NET Core.</p>
        </body>
    </html>
    """,
    contentType: "text/html"
));
#endregion
app.Run();
WriteLine("The server has stopped.");