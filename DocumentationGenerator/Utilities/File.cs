namespace DocumentationGenerator.Utilities;

public static class FileUtil
{
    /// <summary>
    ///     Safetly reads the contents of a text file.
    /// </summary>
    public static string SafeReadAllText(string path, int retries = 5, int delay = 200)
    {
        for (var i = 0; i < retries; i++)
            try
            {
                return File.ReadAllText(path);
            }
            catch (IOException)
            {
                if (i == retries - 1) throw;
                Thread.Sleep(delay);
            }

        return null;
    }

    /// <summary>
    ///     Safetly writes the contents of a text file.
    /// </summary>
    public static void SafeWriteAllText(string path, string contents, int retries = 5, int delay = 200)
    {
        for (var i = 0; i < retries; i++)
            try
            {
                File.WriteAllText(path, contents);
                return;
            }
            catch (IOException)
            {
                if (i == retries - 1) throw;
                Thread.Sleep(delay);
            }
    }
}