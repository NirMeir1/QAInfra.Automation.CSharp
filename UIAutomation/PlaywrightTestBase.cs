using Microsoft.Playwright;
using NUnit.Framework;

namespace UIAutomation;

public abstract class PlaywrightTestBase
{
    protected IPlaywright PlaywrightInstance = default!;
    protected IBrowser Browser = default!;
    protected IBrowserContext Context = default!;
    protected IPage Page = default!;

    [OneTimeSetUp]
    public async Task GlobalSetUp()
    {
        PlaywrightInstance = await Microsoft.Playwright.Playwright.CreateAsync();
        Browser = await PlaywrightInstance.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
            Args = new[] { "--ignore-certificate-errors" }
        });
    }

    [SetUp]
    public async Task SetUpAsync()
    {
        Context = await Browser.NewContextAsync(new BrowserNewContextOptions { IgnoreHTTPSErrors = true });
        Page = await Context.NewPageAsync();
    }

    [TearDown]
    public async Task TearDownAsync()
    {
        await Context.CloseAsync();
    }

    [OneTimeTearDown]
    public async Task GlobalTearDownAsync()
    {
        await Browser.CloseAsync();
        PlaywrightInstance.Dispose();
    }
}
