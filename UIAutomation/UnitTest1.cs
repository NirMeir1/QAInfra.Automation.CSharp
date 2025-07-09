using Microsoft.Playwright;

using NUnit.Framework;

namespace UIAutomation;

public class Tests : PlaywrightTestBase
{
    [Test]
    public async Task VerifyTitleContainsPlaywright()
    {
        await Page.GotoAsync("https://playwright.dev");
        StringAssert.Contains("Playwright", await Page.TitleAsync());
    }
}
