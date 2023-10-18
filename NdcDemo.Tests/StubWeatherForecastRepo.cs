using NdcDemo.Data;

namespace NdcDemo.Tests;

internal class StubWeatherForecastRepo : WeatherForecastRepo
{
    private readonly int forecastsToReturn;

    public StubWeatherForecastRepo(int forecastsToReturn)
        => this.forecastsToReturn = forecastsToReturn;

    public override Task<WeatherForecast[]> GetForecasts()
    {
        var startDate = DateOnly.FromDateTime(DateTime.Now);
        var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
        var forecasts = Enumerable
            .Range(1, forecastsToReturn)
            .Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = summaries[Random.Shared.Next(summaries.Length)]
            })
            .ToArray();

        return Task.FromResult(forecasts);
    }
}