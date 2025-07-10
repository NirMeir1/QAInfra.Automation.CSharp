using System.Linq;
using Microsoft.Playwright;
using NUnit.Framework;
using UIAutomation.Infra;

namespace UIAutomation.Tests;

/// <summary>
/// Sample UI tests demonstrating parameterization and failure artifact capture.
/// </summary>
public class SampleUITests : PlaywrightTestBase
{
    [TestCase("https://playwright.dev", "Playwright")]
    [TestCase("https://example.com", "Example Domain")]
    public async Task VerifyTitle(string url, string expected)
    {
        Log.Info($"Navigating to {url}");
        await Page.GotoAsync(url);
        StringAssert.Contains(expected, await Page.TitleAsync());
    }

    [Test]
    public async Task IntentionalFailure()
    {
        await Page.GotoAsync("https://playwright.dev");
        StringAssert.Contains("NonExistent", await Page.TitleAsync());
    }
}

/// <summary>
/// Demonstrates custom per-test setup by adding a cookie before navigation.
/// </summary>
public class CustomContextTests : PlaywrightTestBase
{
    protected override async Task BeforeTestAsync()
    {
        await Context.AddCookiesAsync(new[]
        {
            new Cookie { Name = "demo", Value = "1", Url = "https://playwright.dev" }
        });
    }

    [Test]
    public async Task CookieIsSet()
    {
        await Page.GotoAsync("https://playwright.dev");
        var cookies = await Context.CookiesAsync();
        Assert.That(cookies.Any(c => c.Name == "demo"));
    }
}
