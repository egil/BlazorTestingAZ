namespace BlazorTestingAZ.Tests.End2EndTesting.SnapshotTesting;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
internal class CounterPageSnapshotTest : BlazorPageTest<Program>
{
    [Test]
    public async Task Counter_page()
    {
        // Arrange
        await Page.GotoPreRenderedAsync("counter");

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