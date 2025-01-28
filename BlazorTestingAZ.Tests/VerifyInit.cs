using System.Runtime.CompilerServices;
using AngleSharp.Diffing;

namespace BlazorTestingAZ.Tests;

public static class VerifyInit
{
    [ModuleInitializer]
    public static void InitPlaywright()
    {
        ClipboardAccept.Enable();
        VerifyAngleSharpDiffing.Initialize(options =>
        {
            options.AddDefaultOptions();
        });
        VerifyPlaywright.Initialize();
        VerifyBunit.Initialize(excludeComponent: true);
    }
}

public class VerifyInitTest
{
    [Fact]
    public static Task Run() =>
        VerifyChecks.Run();
}
