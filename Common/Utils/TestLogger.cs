using NUnit.Framework;

namespace Common.Utils;

/// <summary>
/// Simple logger that writes timestamped messages to the current test output.
/// </summary>
public class TestLogger
{
    public static void Info(string message)
    {
        TestContext.WriteLine($"{DateTime.UtcNow:O} [INFO] {message}");
    }

    public static void Error(string message)
    {
        TestContext.WriteLine($"{DateTime.UtcNow:O} [ERROR] {message}");
    }
}
