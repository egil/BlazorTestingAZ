using Microsoft.Playwright;

namespace NdcDemo.Tests.End2EndTesting.SnapshotTesting;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
internal class CounterPageTest : BlazorPageTest<Program>
{
    [Test]
    public async Task Counter_page()
    {
        // Arrange
        await Page.GotoPreRenderedAsync("counter");

        // Verify page content only
        await Verify(await Page.ContentAsync(), "html");

        // Verify page and create screenshot
        // await Verify(Page).PageScreenshotOptions(new()
        // {
        //     Quality = 50,
        //     Type = ScreenshotType.Jpeg
        // });
    }

    // Run diffenginetray to manage diffs
}