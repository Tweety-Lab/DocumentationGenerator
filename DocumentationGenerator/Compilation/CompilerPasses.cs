using System.Text.RegularExpressions;
using DocumentationGenerator.FileUtilities.HTML;
using DocumentationGenerator.FileUtilities.Markdown;
using DocumentationGenerator.Serializing;

namespace DocumentationGenerator.Compilation;

/// <summary>
///     Converts Markdown to HTML.
/// </summary>
public class MarkdownConversionPass : ICompilerPass
{
    public void Execute(Page page)
    {
        page.HTMLDocumentationContents = MarkdownUtil.ConvertMarkdownToHTML(page.MarkdownContents);
    }
}

/// <summary>
///     Compiles the Navigation Bar.
/// </summary>
public class NavBarGenerationPass : ICompilerPass
{
    public void Execute(Page page)
    {
        var navbarHTML = NavBarUtil.ConvertNavBarToHTML(Program.Builder.NavBar);
        var compiledNavBar = HTMLUtil.ReplaceKeyword(
            HTMLTemplates.NavBarTemplate, "{{NAVBAR_CONTENT}}", navbarHTML);

        page.HTMLNavBarContents = compiledNavBar;
    }
}

/// <summary>
///     Add Compiler Variables to the page.
/// </summary>
public class CompilerVariablePass : ICompilerPass
{
    public void Execute(Page page)
    {
        // Define compiler variable and their values
        var replacements = new Dictionary<string, string>
        {
            { "{{NAVBAR}}", page.HTMLNavBarContents },
            { "{{HTML_DOCUMENTATION}}", page.HTMLDocumentationContents },
            { "{{DOCUMENTATION_TITLE}}", page.Title }
        };

        // Replace compiler variables with their value
        page.HTMLContents = HTMLUtil.ReplaceKeywords(HTMLTemplates.PageTemplate, replacements);
    }
}

/// <summary>
///     Removes all comments from the page.
/// </summary>
public class CommentRemovalPass : ICompilerPass
{
    public void Execute(Page page)
    {
        page.HTMLContents = Regex.Replace(page.HTMLContents, "<!--.*?-->", "", RegexOptions.Singleline);
    }
}