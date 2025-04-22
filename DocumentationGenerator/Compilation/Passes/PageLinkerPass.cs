using DocumentationGenerator.Serializing;
using System.Text.RegularExpressions;

namespace DocumentationGenerator.Compilation.Passes;

/// <summary>
/// Converts .md links in HTML content to .html
/// </summary>
public class PageLinkerPass : ICompilerPass
{
    private static readonly Regex MdHrefRegex = new(@"href\s*=\s*[""']([^""']+\.md)[""']", RegexOptions.IgnoreCase);

    public void Execute(Page page)
    {
        if (string.IsNullOrWhiteSpace(page.HTMLDocumentationContents))
            return;

        // Replace all .md links with .html
        page.HTMLDocumentationContents = MdHrefRegex.Replace(page.HTMLDocumentationContents, match =>
        {
            var mdPath = match.Groups[1].Value;
            var htmlPath = mdPath.Substring(0, mdPath.Length - 3) + ".html"; // replace .md with .html
            return $"href=\"{htmlPath}\"";
        });
    }
}
