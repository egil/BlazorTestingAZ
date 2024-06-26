﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using BlazorTestingAZ.Data;

namespace BlazorTestingAZ.Tests.End2EndTesting;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
internal class WeatherPageServicesReplacedTest : BlazorPageTest<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Here be dragons! Hint - "auto"
            services.AddScoped<WeatherForecastRepo>(
                _ => new StubWeatherForecastRepo(forecastsToReturn: 1));
        });

        // Set up dev containers, spin up databases, etc.
    }

    [Test]
    public async Task WeatherForecastTable_LoadsAndDisplaysData_OnPageInitialization()
    {
        // Arrange
        await Page.GotoAsync("weather");

        // Act
        await Page.WaitForSelectorAsync("table");

        // Assert
        var rows = await Page.Locator("table>tbody>tr").CountAsync();
        Assert.That(rows, Is.EqualTo(1));
    }
}
