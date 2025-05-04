var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

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


app.Run();
WriteLine("The server has stopped.");