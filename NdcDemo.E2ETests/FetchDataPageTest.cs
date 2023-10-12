using NdcDemo.E2ETestsNunit.Playwright.Blazor;

namespace NdcDemo.E2ETestsNunit;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
internal class FetchDataPageTest : BlazorPageTest<Program>
{
    [Test]
    public async Task WeatherForecastTable_LoadsAndDisplaysData_OnPageInitialization()
    {
        // Start tracing before creating / navigating a page.
        await Context.Tracing.StartAsync(new()
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });

        await Page.GotoAsync("weather");

        await Page.WaitForSelectorAsync("h1 >> text=Weather");

        await Page.WaitForSelectorAsync("table>tbody>tr");

        Assert.That(await Page.Locator("p+table>tbody>tr").CountAsync(), Is.EqualTo(5));

        // Stop tracing and export it into a zip archive.
        await Context.Tracing.StopAsync(new()
        {
            Path = nameof(WeatherForecastTable_LoadsAndDisplaysData_OnPageInitialization) + "-trace.zip"
        });
    }
}