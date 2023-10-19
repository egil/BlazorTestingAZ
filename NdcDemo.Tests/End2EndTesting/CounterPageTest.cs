namespace NdcDemo.Tests.End2EndTesting;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
internal class CounterPageTest : BlazorPageTest<Program>
{
    [Test]
    public async Task Count_Increments_WhenButtonIsClicked()
    {
        // Arrange
        await Page.GotoPreRenderedAsync("counter");

        // Act
        await Page
            .GetByRole(AriaRole.Button, new PageGetByRoleOptions() { Name = "Click me" })
            .ClickAsync();

        // Assert
        ILocator status = Page.GetByRole(AriaRole.Status);
        await Expect(status).ToHaveTextAsync("Current count: 1");
    }
}
