using NUnit.Framework;
using System.IO;
namespace Common.Utils;

public static class FileHelper
{
    /// <summary>
    /// Returns a directory under TestContext.ResultsDirectory to store artifacts.
    /// Ensures the directory exists.
    /// </summary>
    public static string EnsureArtifactsDirectory(string? subFolder = null)
    {
        var dir = Path.Combine(TestContext.CurrentContext.WorkDirectory, "TestResults", subFolder ?? string.Empty);
        Directory.CreateDirectory(dir);
        return dir;
    }
}
