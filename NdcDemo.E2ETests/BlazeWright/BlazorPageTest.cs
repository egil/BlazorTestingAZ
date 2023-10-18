using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace BlazeWright;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class BlazorPageTest<TProgram> : BrowserTest
    where TProgram : class
{
    private BlazorApplicationFactory<TProgram>? host;

    public IBrowserContext Context { get; private set; } = null!;

    public IPage Page { get; private set; } = null!;

    public BlazorApplicationFactory<TProgram> Host
    {
        get
        {
            host ??= CreateHostFactory() ?? new BlazorApplicationFactory<TProgram>();
            return host;
        }
    }

    public virtual BlazorApplicationFactory<TProgram> CreateHostFactory()
        => new BlazorApplicationFactory<TProgram>();

    public virtual BrowserNewContextOptions ContextOptions() => null!;

    [SetUp]
    public async Task PageSetup()
    {
        var options = ContextOptions() ?? new BrowserNewContextOptions();
        options.BaseURL = Host.ServerAddress;
        options.IgnoreHTTPSErrors = true;

        Context = await NewContext(options).ConfigureAwait(false);
        Page = await Context.NewPageAsync().ConfigureAwait(false);
    }

    [TearDown]
    public async Task HostTearDown()
    {
        if (host is { } currentHost)
        {
            host = null;

            // Navigate to about:blank to ensure any SignalR
            // connections are dropped.
            await Page.GotoAsync("about:blank");
            await Context.DisposeAsync();
            await currentHost.DisposeAsync();
        }
    }
}
