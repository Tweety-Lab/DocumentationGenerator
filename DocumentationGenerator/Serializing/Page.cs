using DocumentationGenerator.Compilation;
using DocumentationGenerator.Compilation.Passes;
using DocumentationGenerator.Utilities;

namespace DocumentationGenerator.Serializing;

/// <summary>
///     Markdown Page
/// </summary>
public class Page
{
    public string Title { get; set; }
    public string MarkdownContents { get; set; }

    // HTML Content
    public string HTMLDocumentationContents { get; set; }
    public string HTMLNavBarContents { get; set; }
    public string HTMLContents { get; set; }

    public void WriteToFile(string path)
    {
        // Get directory
        var directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory)) Directory.CreateDirectory(directory);

        // Compile Page if it's not already compiled
        if (HTMLContents == null)
            Compiler.CompilePage(this);

        // Write to file
        FileUtil.SafeWriteAllText(path, HTMLContents);
    }
}