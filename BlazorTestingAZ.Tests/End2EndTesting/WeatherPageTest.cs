namespace BlazorTestingAZ.Tests.End2EndTesting;

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
        // Uses WaitFor selector to poll DOM until weather data loads
        await Page.WaitForSelectorAsync("table");

        // Assert
        var rows = await Page.Locator("table>tbody>tr").CountAsync();        
        Assert.That(rows, Is.EqualTo(5)); // is "5" stable?
    }
}