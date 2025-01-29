namespace BlazorTestingAZ.Tests.End2EndTesting.SnapshotTesting;

public class CounterPageSnapshotTest : BlazorPageTest<Program>
{
    [Fact]
    public async Task Counter_page()
    {
        // Arrange
        await Page.GotoBlazorServerPageAsync("counter");

        // Get part of the page to snapshot
        ILocator main = Page.GetByRole(AriaRole.Main);
        string mainHtml = await main.InnerHTMLAsync();

        // Verify page content only, using semantic comparison
        await Verify(mainHtml, "html");

        // Verify by comparing a screenshot to the rendered page
        //await Verify(Page).PageScreenshotOptions(new()
        //{
        //    Quality = 50,
        //    Type = ScreenshotType.Jpeg
        //});
    }
}