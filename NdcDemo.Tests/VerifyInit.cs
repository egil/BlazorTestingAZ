using System.Runtime.CompilerServices;

namespace NdcDemo.Tests;

public static class VerifyInit
{
    [ModuleInitializer]
    public static void InitPlaywright()
    {
        VerifyPlaywright.Initialize();
        VerifyBunit.Initialize(excludeComponent: true);
    }
}

