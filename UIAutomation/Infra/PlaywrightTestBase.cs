using Common.Utils;
using Microsoft.Playwright;
using NUnit.Framework;

namespace UIAutomation.Infra;

/// <summary>
/// Base class for Playwright tests providing isolated browser context per test,
/// logging and automatic artifact capture on failure.
/// </summary>
[Parallelizable(ParallelScope.All)]
public abstract class PlaywrightTestBase
{
    protected IPlaywright PlaywrightInstance = default!;
    protected IBrowser Browser = default!;
    protected IBrowserContext Context = default!;
    protected IPage Page = default!;
    protected TestLogger Log = default!;

    [OneTimeSetUp]
    public async Task GlobalSetUp()
    {
        PlaywrightInstance = await Microsoft.Playwright.Playwright.CreateAsync();
        Browser = await CreateBrowserAsync();
    }

    /// <summary>
    /// Override to change browser type or launch options via environment variable BROWSER.
    /// </summary>
    protected virtual async Task<IBrowser> CreateBrowserAsync()
    {
        var browserName = Environment.GetEnvironmentVariable("BROWSER")?.ToLowerInvariant() ?? "chromium";
        var options = CreateBrowserOptions();
        return browserName switch
        {
            "firefox" => await PlaywrightInstance.Firefox.LaunchAsync(options),
            "webkit" => await PlaywrightInstance.Webkit.LaunchAsync(options),
            _ => await PlaywrightInstance.Chromium.LaunchAsync(options)
        };
    }

    /// <summary>
    /// Customize browser launch options. Override in derived classes.
    /// </summary>
    protected virtual BrowserTypeLaunchOptions CreateBrowserOptions() => new()
    {
        Headless = true,
        Args = new[] { "--ignore-certificate-errors" }
    };

    /// <summary>
    /// Customize context options. Override for cookies, permissions, etc.
    /// </summary>
    protected virtual BrowserNewContextOptions CreateContextOptions() => new() { IgnoreHTTPSErrors = true };

    /// <summary>
    /// Hook executed after context and page are created.
    /// </summary>
    protected virtual Task BeforeTestAsync() => Task.CompletedTask;

    /// <summary>
    /// Hook executed before context is disposed.
    /// </summary>
    protected virtual Task AfterTestAsync() => Task.CompletedTask;

    [SetUp]
    public async Task SetUpAsync()
    {
        Log = new TestLogger(TestContext.CurrentContext);
        Context = await Browser.NewContextAsync(CreateContextOptions());
        Page = await Context.NewPageAsync();
        await BeforeTestAsync();
    }

    [TearDown]
    public async Task TearDownAsync()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
        {
            var dir = FileHelper.EnsureArtifactsDirectory(TestContext.CurrentContext.Test.Name);
            var screenshotPath = Path.Combine(dir, "screenshot.png");
            var htmlPath = Path.Combine(dir, "page.html");
            await Page.ScreenshotAsync(new PageScreenshotOptions { Path = screenshotPath, FullPage = true });
            await File.WriteAllTextAsync(htmlPath, await Page.ContentAsync());
            Log.Error($"Saved failure artifacts to {dir}");
        }
        await AfterTestAsync();
        await Context.CloseAsync();
    }

    [OneTimeTearDown]
    public async Task GlobalTearDownAsync()
    {
        await Browser.CloseAsync();
        PlaywrightInstance.Dispose();
    }
}
