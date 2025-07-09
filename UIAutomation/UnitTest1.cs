using Microsoft.Playwright;

using NUnit.Framework;

namespace UIAutomation;

public class Tests
{
    [Test]
    public async Task VerifyTitleContainsPlaywright()
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
            Args = ["--ignore-certificate-errors"]
        });
        var context = await browser.NewContextAsync(new BrowserNewContextOptions { IgnoreHTTPSErrors = true });
        var page = await context.NewPageAsync();
        await page.GotoAsync("https://playwright.dev");
        StringAssert.Contains("Playwright", await page.TitleAsync());
    }
}
