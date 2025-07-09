namespace UIAutomation;

public class Tests
{
    [Test]
    public async Task VerifyTitleContainsPlaywright()
    {
        using var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new Microsoft.Playwright.BrowserTypeLaunchOptions
        {
            Headless = true,
            Args = new[] { "--ignore-certificate-errors" }
        });
        var context = await browser.NewContextAsync(new Microsoft.Playwright.BrowserNewContextOptions { IgnoreHTTPSErrors = true });
        var page = await context.NewPageAsync();
        await page.GotoAsync("https://playwright.dev");
        StringAssert.Contains("Playwright", await page.TitleAsync());
    }
}
