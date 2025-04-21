using DocumentationGenerator.Compilation;
using DocumentationGenerator.Utilities;

namespace DocumentationGenerator.Serializing;

/// <summary>
///     Markdown Page
/// </summary>
public class Page
{
    // All compiler passes to run
    private static readonly List<ICompilerPass> CompilerPasses = new()
    {
        new MarkdownConversionPass(),
        new NavBarGenerationPass(),
        new CompilerVariablePass(),
        new CommentRemovalPass()
    };

    public string Title { get; set; }
    public string MarkdownContents { get; set; }

    // HTML Content
    public string HTMLDocumentationContents { get; set; }
    public string HTMLNavBarContents { get; set; }
    public string HTMLContents { get; set; }

    public void WriteToFile(string path)
    {
        var directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory)) Directory.CreateDirectory(directory);

        if (HTMLContents == null)
            CompilePage();

        FileUtil.SafeWriteAllText(path, HTMLContents);
    }


    /// <summary>
    ///     Compile the page.
    /// </summary>
    public void CompilePage()
    {
        HTMLDocumentationContents ??= MarkdownContents;

        // Apply compiler passes
        foreach (var pass in CompilerPasses) pass.Execute(this);
    }

    public static void RegisterCompilerPass(ICompilerPass pass)
    {
        CompilerPasses.Add(pass);
    }
}