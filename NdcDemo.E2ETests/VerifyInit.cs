using System.Runtime.CompilerServices;

namespace NdcDemo.E2ETests;

public static class VerifyInit
{
    [ModuleInitializer]
    public static void InitPlaywright()
    {
        VerifyPlaywright.Initialize();
        VerifyBunit.Initialize(excludeComponent: true);
    }
}

