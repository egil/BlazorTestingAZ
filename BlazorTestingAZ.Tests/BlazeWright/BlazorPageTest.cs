using Microsoft.AspNetCore.Hosting;
using Microsoft.Playwright.Xunit;

namespace BlazeWright;

public class BlazorPageTest<TProgram> : BrowserTest
    where TProgram : class
{
    private BlazorApplicationFactory<TProgram>? host;
    private IPage? page;
    private IBrowserContext? context;

    public IBrowserContext Context => context ?? throw new InvalidOperationException("Setup has not been run.");

    public IPage Page => page ?? throw new InvalidOperationException("Setup has not been run.");

    public BlazorApplicationFactory<TProgram> Host => host ?? throw new InvalidOperationException("Setup has not been run.");

    public virtual BlazorApplicationFactory<TProgram> CreateHostFactory()
        => new BlazorApplicationFactory<TProgram>(ConfigureWebHost);

    public virtual BrowserNewContextOptions ContextOptions() => null!;

    protected virtual void ConfigureWebHost(IWebHostBuilder builder) { }

    public override async Task InitializeAsync()
    {
        host = new BlazorApplicationFactory<TProgram>(ConfigureWebHost);
        await host.InitializeAsync();
        await base.InitializeAsync();

        var options = ContextOptions() ?? new BrowserNewContextOptions();
        options.BaseURL = Host.ServerAddress;
        options.IgnoreHTTPSErrors = true;

        context = await NewContext(options);
        page = await Context.NewPageAsync();
    }

    public override async Task DisposeAsync()
    {
        if (page is not null)
        {
            await page.CloseAsync();
        }

        if (context is not null)
        {
            await context.DisposeAsync();
        }

        if (host is not null)
        {
            await host.DisposeAsync();
            host = null;
        }
    }
}
