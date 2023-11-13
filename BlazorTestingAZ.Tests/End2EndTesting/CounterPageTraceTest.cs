namespace BlazorTestingAZ.Tests.End2EndTesting;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
internal class CounterPageTraceTest : BlazorPageTest<Program>
{
    [Test]
    public async Task Count_Increments_WhenButtonIsClicked()
    {
        // Arrange
        await Context.Tracing.StartAsync(new()
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });

        // Act
        await Page.GotoPreRenderedAsync("counter");
        await Page
            .GetByRole(AriaRole.Button, new PageGetByRoleOptions() { Name = "Click me" })
            .ClickAsync();

        // Assert
        ILocator status = Page.GetByRole(AriaRole.Status);
        await Expect(status).ToHaveTextAsync("Current count: 1");

        await Context.Tracing.StopAsync(new()
        {
            Path = nameof(Count_Increments_WhenButtonIsClicked) + ".trace.zip"
        });

        // View trace:
        // .\NdcDemo.Tests\bin\Debug\net8.0\playwright.ps1 show-trace .\NdcDemo.Tests\bin\Debug\net8.0\Count_Increments_WhenButtonIsClicked.trace.zip
    }
}
