using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NdcDemo.Data;

namespace NdcDemo.E2ETests;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
internal class WeatherPageServicesReplacedTest : BlazorPageTest<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddScoped<WeatherForecastRepo, DummyWeatherForecastRepo>();
        });
    }

    [Test]
    public async Task WeatherForecastTable_LoadsAndDisplaysData_OnPageInitialization()
    {
        // Arrange
        await Page.GotoAsync("weather");

        // Act
        await Page.WaitForSelectorAsync("h1 >> text=Weather");
        await Page.WaitForSelectorAsync("table>tbody>tr");
        var rows = await Page.Locator("p+table>tbody>tr").CountAsync();

        // Assert
        Assert.That(rows, Is.EqualTo(1));
    }
}

internal class DummyWeatherForecastRepo : WeatherForecastRepo
{
    public override Task<WeatherForecast[]> GetForecasts()
    {
        WeatherForecast[] result = [new()
        {
            Date = DateOnly.FromDateTime(DateTime.UtcNow),
            Summary = "Heavy rain",
            TemperatureC = 20
        }];
        return Task.FromResult(result);
    }
}