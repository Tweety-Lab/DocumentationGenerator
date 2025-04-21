using System.Text.RegularExpressions;
using DocumentationGenerator.Serializing;

namespace DocumentationGenerator.Compilation.Passes;

/// <summary>
/// Removes all comments from the page.
/// </summary>
public class CommentRemovalPass : ICompilerPass
{
    public void Execute(Page page)
    {
        page.HTMLContents = Regex.Replace(page.HTMLContents, "<!--.*?-->", "", RegexOptions.Singleline);
    }
}