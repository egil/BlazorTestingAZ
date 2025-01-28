namespace BlazorTestingAZ.Tests.End2EndTesting;

public class CounterPageTraceTest : BlazorPageTest<Program>
{
    [Fact]
    public async Task Count_Increments_WhenButtonIsClicked()
    {
        // Arrange
        // Explicitly enable tracing during development
        await Context.Tracing.StartAsync(new()
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });

        // Act
        await Page.GotoBlazorServerPageAsync("counter");
        await Page
            .GetByRole(AriaRole.Button, new PageGetByRoleOptions() { Name = "Click me" })
            .ClickAsync();

        // Assert
        ILocator status = Page.GetByRole(AriaRole.Status);
        await Expect(status).ToHaveTextAsync("Current count: 1");

        // Stop tracing and write result to zip file
        await Context.Tracing.StopAsync(new()
        {
            Path = nameof(Count_Increments_WhenButtonIsClicked) + ".trace.zip"
        });

        // View trace:
        // .\BlazorTestingAZ.Tests\bin\Debug\net9.0\playwright.ps1 show-trace .\BlazorTestingAZ.Tests\bin\Debug\net9.0\Count_Increments_WhenButtonIsClicked.trace.zip
    }
}
