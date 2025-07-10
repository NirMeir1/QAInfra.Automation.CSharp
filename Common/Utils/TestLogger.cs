using NUnit.Framework;

namespace Common.Utils;

/// <summary>
/// Simple logger that writes timestamped messages to the current test output.
/// </summary>
public class TestLogger
{
    private readonly TestContext _context;

    public TestLogger(TestContext context)
    {
        _context = context;
    }

    public void Info(string message)
    {
        _context.WriteLine($"{DateTime.UtcNow:O} [INFO] {message}");
    }

    public void Error(string message)
    {
        _context.WriteLine($"{DateTime.UtcNow:O} [ERROR] {message}");
    }
}
