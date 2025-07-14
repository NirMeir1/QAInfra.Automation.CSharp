using Common.Utils;
using Microsoft.Playwright;
using NUnit.Framework;
using UIAutomation.Infra;

namespace UIAutomation.Tests;

/// <summary>
/// Sample UI tests demonstrating parameterization and failure artifact capture.
/// </summary>
public class SampleUITests : PlaywrightTestBase
{
    [TestCase("https://demo.playwright.dev/todomvc/", "TodoMVC")]
    [TestCase("https://example.com", "Example Domain")]
    public async Task VerifyTitle(string url, string expected)
    {
        TestLogger.Info($"Navigating to {url}");
        await Page.GotoAsync(url, new PageGotoOptions { WaitUntil = WaitUntilState.NetworkIdle });
        StringAssert.Contains(expected, await Page.TitleAsync());
    }

    // [Test]
    // public async Task IntentionalFailure()
    // {
    //     await Page.GotoAsync("https://demo.playwright.dev/todomvc/");
    //     StringAssert.Contains("NonExistent", await Page.TitleAsync());
    // }
}
