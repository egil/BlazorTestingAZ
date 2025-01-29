namespace BlazorTestingAZ.Tests.End2EndTesting.SnapshotTesting;

public class CounterPageAriaSnapshotTest : BlazorPageTest<Program>
{
    [Fact]
    public async Task Counter_page()
    {
        // Arrange
        await Page.GotoBlazorServerPageAsync("counter");

        // Act
        await Page
            .GetByRole(AriaRole.Button, new PageGetByRoleOptions() { Name = "Click me" })
            .ClickAsync();

        // Assert
        ILocator main = Page.GetByRole(AriaRole.Main);
        await Expect(main).ToMatchAriaSnapshotAsync("""
            - main:
                - heading "Counter" [level=1]
                - status: "Current count: 1"
                - button "Click me"
            """);
    }
}