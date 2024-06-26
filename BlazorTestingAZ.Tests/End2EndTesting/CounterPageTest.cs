namespace BlazorTestingAZ.Tests.End2EndTesting;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
internal class CounterPageTest : BlazorPageTest<Program>
{
    [Test]
    public async Task Count_Increments_WhenButtonIsClicked()
    {
        // Arrange
        // Tip: create reusable Goto methods that incapsulate cross cutting concerns,
        // e.g. GotoPageAsUser(url, userName)
        await Page.GotoPreRenderedAsync("counter");

        // Act
        // Finding the element by role makes our tests more resilient
        // to refactoring since we can change the HTML element used
        // without breaking the test, as long as the element has the same
        // role, implicitly or explicitly.
        // Learn more about this frontend testing strategy
        // at https://testing-library.com/docs/
        await Page
            .GetByRole(AriaRole.Button, new PageGetByRoleOptions() { Name = "Click me" })
            .ClickAsync();

        // Assert
        ILocator status = Page.GetByRole(AriaRole.Status);
        await Expect(status).ToHaveTextAsync("Current count: 1");
    }
}
