public partial class Program
{
    /// <summary>
    /// Contains all possible weather summaries.
    /// </summary>
    static string[] summaries =
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    /// <summary>
    /// Get (pseudo) weather forecast for a given number of days
    /// </summary>
    /// <param name="days">Number of days to forecast</param>
    /// <returns>new array of weather forecasts</returns>
    internal static WeatherForecast[]? GetWeather(int days)
    {
        // Create all weather forecasts
        WeatherForecast[]? forecast = Enumerable.Range(1, days)
            .Select(index => new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Length)]
            ))
            .ToArray();

        return forecast;
    }

    /// <summary>
    /// Can be used to store data of a single weather forecast.
    /// </summary>
    internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
