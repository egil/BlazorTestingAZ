using Microsoft.Playwright;

namespace NdcDemo.E2ETests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
internal class CounterPageTest : BlazorPageTest<Program>
{
    [Test]
    public async Task Count_Increments_WhenButtonIsClicked()
    {
        // Start tracing before creating / navigating a page.
        await Context.Tracing.StartAsync(new()
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });

        await Page.GotoPreRenderedAsync("counter");

        await Page.GetByRole(AriaRole.Button, new() { Name = "Click me" }).ClickAsync();

        await Expect(Page.GetByRole(AriaRole.Status)).ToHaveTextAsync("Current count: 1");

        // Stop tracing and export it into a zip archive.
        await Context.Tracing.StopAsync(new()
        {
            Path = nameof(Count_Increments_WhenButtonIsClicked) + ".trace.zip"
        });
    }
}
