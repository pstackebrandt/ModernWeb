var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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