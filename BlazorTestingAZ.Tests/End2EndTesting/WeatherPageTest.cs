﻿namespace BlazorTestingAZ.Tests.End2EndTesting;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
internal class WeatherPageTest : BlazorPageTest<Program>
{
    [Test]
    public async Task WeatherForecastTable_LoadsAndDisplaysData_OnPageInitialization()
    {
        // Arrange
        await Page.GotoAsync("weather");

        // Act
        await Page.WaitForSelectorAsync("h1 >> text=Weather");
        await Page.WaitForSelectorAsync("table>tbody>tr");

        // Assert
        var rows = await Page.Locator("p+table>tbody>tr").CountAsync();
        Assert.That(rows, Is.EqualTo(5));
    }
}