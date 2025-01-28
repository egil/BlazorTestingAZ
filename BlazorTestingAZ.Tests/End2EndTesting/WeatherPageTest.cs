namespace BlazorTestingAZ.Tests.End2EndTesting;

public class WeatherPageTest : BlazorPageTest<Program>
{
    [Fact]
    public async Task WeatherForecastTable_LoadsAndDisplaysData_OnPageInitialization()
    {
        // Arrange
        await Page.GotoAsync("weather");

        // Act
        // Uses WaitFor selector to poll DOM until weather data loads
        await Page.WaitForSelectorAsync("table");

        // Assert
        var rows = await Page.Locator("table>tbody>tr").CountAsync();        
        Assert.Equal(5, rows); // is "5" stable?
    }
}