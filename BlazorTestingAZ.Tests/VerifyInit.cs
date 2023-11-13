using System.Runtime.CompilerServices;

namespace BlazorTestingAZ.Tests;

public static class VerifyInit
{
    [ModuleInitializer]
    public static void InitPlaywright()
    {
        VerifyPlaywright.Initialize();
        VerifyBunit.Initialize(excludeComponent: true);
    }
}

