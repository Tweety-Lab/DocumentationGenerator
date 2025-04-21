using DocumentationGenerator.FileUtilities.Markdown;
using DocumentationGenerator.Serializing;

namespace DocumentationGenerator.Compilation;

/// <summary>
/// Converts Markdown to HTML.
/// </summary>
public class MarkdownConversionPass : ICompilerPass
{
    public void Execute(Page page)
    {
        page.HTMLDocumentationContents = MarkdownUtil.ConvertMarkdownToHTML(page.MarkdownContents);
    }
}