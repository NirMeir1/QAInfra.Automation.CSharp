using NUnit.Framework;
namespace Common.Utils;

public static class FileHelper
{
    /// <summary>
    /// Returns a directory under TestContext.ResultsDirectory to store artifacts.
    /// Ensures the directory exists.
    /// </summary>
    public static string EnsureArtifactsDirectory(string subFolder)
    {
        // Replace invalid filename characters with underscores
        foreach (var c in Path.GetInvalidFileNameChars())
        {
            subFolder = subFolder.Replace(c, '_');
        }
        var dir = Path.Combine("TestResults", subFolder);
        Directory.CreateDirectory(dir);
        return dir;
    }

}
