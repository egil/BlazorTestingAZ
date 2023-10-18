using System.Runtime.CompilerServices;

namespace NdcDemo.E2ETests.SnapshotTesting;

public static class VerifyInit
{
    [ModuleInitializer]
    public static void InitPlaywright()
        => VerifyPlaywright.Initialize();
}

