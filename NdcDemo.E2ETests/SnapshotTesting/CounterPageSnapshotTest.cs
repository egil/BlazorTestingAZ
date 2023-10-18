using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerifyTests;
using VerifyTests.Playwright;

namespace NdcDemo.E2ETests.SnapshotTesting;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
internal class CounterPageTest : BlazorPageTest<Program>
{
    [Test]
    public async Task Counter_page()
    {
        // Arrange
        await Page.GotoPreRenderedAsync("counter");

        await Verify(Page)
            .PageScreenshotOptions(
                new()
                {
                    Quality = 50,
                    Type = ScreenshotType.Jpeg
                });
    }

}