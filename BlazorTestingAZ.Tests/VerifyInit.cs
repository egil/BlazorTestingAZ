using System.Runtime.CompilerServices;
using AngleSharp.Diffing;
using AngleSharp.Dom;
using DiffEngine;
using VerifyTests.AngleSharp;

namespace BlazorTestingAZ.Tests;

public static class VerifyInit
{
    [ModuleInitializer]
    public static void InitPlaywright()
    {
        ClipboardAccept.Enable();
        DiffTools.UseOrder(DiffTool.VisualStudioCode, DiffTool.VisualStudio, DiffTool.Rider);
        
        VerifyAngleSharpDiffing.Initialize(options =>
        {
            options.AddDefaultOptions();
        });
        
        HtmlPrettyPrint.All(nodes => nodes.ScrubComments());

        VerifyPlaywright.Initialize();
        VerifyBunit.Initialize(excludeComponent: true);
    }

    public static INodeList ScrubComments(this INodeList nodes)
    {
        foreach (var node in nodes.DescendantsAndSelf())
        {
            if (node is IComment comment)
            {
                comment.RemoveFromParent();
            }
        }

        return nodes;
    }
}

public class VerifyInitTest
{
    [Fact]
    public static Task Run() =>
        VerifyChecks.Run();
}
