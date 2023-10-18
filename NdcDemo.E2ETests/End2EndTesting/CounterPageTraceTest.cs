using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Playwright;

namespace NdcDemo.E2ETests.End2EndTesting;

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
        await Expect(Page.GetByRole(AriaRole.Status)).ToHaveTextAsync("Current count: 1");

        await Context.Tracing.StopAsync(new()
        {
            Path = nameof(Count_Increments_WhenButtonIsClicked) + ".trace.zip"
        });
    }
}
