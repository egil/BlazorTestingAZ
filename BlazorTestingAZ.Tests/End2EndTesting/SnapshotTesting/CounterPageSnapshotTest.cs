namespace BlazorTestingAZ.Tests.End2EndTesting.SnapshotTesting;

public class CounterPageSnapshotTest : BlazorPageTest<Program>
{
    [Fact]
    public async Task Counter_page()
    {
        // Arrange
        await Page.GotoBlazorServerPageAsync("counter");

        // Verify page content only,
        // using semantic comparison
        IElementHandle? bodyElement = await Page.QuerySelectorAsync("body");
        string bodyHtml = await bodyElement!.InnerHTMLAsync();
        await Verify(bodyHtml, "html");

        // Verify by comparing a screenshot to the rendered page
        //await Verify(Page).PageScreenshotOptions(new()
        //{
        //    Quality = 50,
        //    Type = ScreenshotType.Jpeg
        //});
    }
}