using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace NdcDemo.E2ETests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
internal class CounterPageTestManual
{
    [Test]
    public async Task Count_Increments_WhenButtonIsClicked()
    {
        // Arrange
        using BlazorApplicationFactory<Program> host = new BlazorApplicationFactory<Program>();
        using IPlaywright playwright = await Playwright.CreateAsync();
        await using IBrowser? browser = await playwright.Chromium.LaunchAsync();
        BrowserNewContextOptions contextOptions = new BrowserNewContextOptions
        {
            BaseURL = host.ServerAddress,
            IgnoreHTTPSErrors = true,
        };
        IBrowserContext context = await browser.NewContextAsync(contextOptions);
        IPage page = await context.NewPageAsync();

        // Act
        await page.GotoAsync("counter", new() { WaitUntil = WaitUntilState.NetworkIdle });
        await page
            .GetByRole(AriaRole.Button, new PageGetByRoleOptions() { Name = "Click me" })
            .ClickAsync();

        // Assert
        ILocator status = page.GetByRole(AriaRole.Status);
        await Expect(status).ToHaveTextAsync("Current count: 1");
    }
}