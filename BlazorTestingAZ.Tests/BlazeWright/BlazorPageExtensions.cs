using System.Diagnostics;

namespace BlazeWright;

public static class BlazorPageExtensions
{
    [DebuggerHidden]
    [DebuggerStepThrough]
    public static Task<IResponse?> GotoBlazorServerPageAsync(this IPage page, string url)
        => page.GotoAsync(url, new() { WaitUntil = WaitUntilState.NetworkIdle });
}
