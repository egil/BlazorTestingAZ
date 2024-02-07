using static Microsoft.Playwright.Assertions;

namespace BlazorTestingAZ.Tests.End2EndTesting;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
internal class CounterPageManualTest
{
    [Test]
    public async Task Count_Increments_WhenButtonIsClicked()
    {
        // Arrange
        // Runs Blazor App referenced by Program, making it
        // available on 127.0.0.1 on a random free port.
        using BlazorApplicationFactory<Program> host = new();

        using IPlaywright playwright = await Playwright.CreateAsync();
        await using IBrowser? browser = await playwright.Chromium.LaunchAsync();
        BrowserNewContextOptions contextOptions = new BrowserNewContextOptions
        {
            // Assigns the base address of the host
            // (cannot be hardcoded due to random chosen port)
            BaseURL = host.ServerAddress,
            // BAF/WAF uses dotnet dev-cert for HTTPS. If
            // that is not trusted on your CI pipeline, this ensures
            // that tests will continue working.
            IgnoreHTTPSErrors = true,
        };

        IBrowserContext context = await browser.NewContextAsync(contextOptions);
        IPage page = await context.NewPageAsync();

        // Go to the counter page, and waits till the network is idle.
        // This is needed when pre-rendering is enabled and using Blazor Server,
        // since the page is not interactive until the SignalR connection to the
        // backend has been established.
        await page.GotoAsync("counter", new() { WaitUntil = WaitUntilState.NetworkIdle });

        // Act
        await page
            .GetByRole(AriaRole.Button, new PageGetByRoleOptions() { Name = "Click me" })
            .ClickAsync();

        // Assert
        ILocator status = page.GetByRole(AriaRole.Status);
        await Expect(status).ToHaveTextAsync("Current count: 1");
    }
}
