using DocumentationGenerator.FileUtilities.HTML;
using DocumentationGenerator.FileUtilities.Markdown;
using DocumentationGenerator.Serializing;
using System.Text.RegularExpressions;

namespace DocumentationGenerator.Compilation
{
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

    /// <summary>
    /// Compiles the Navigation Bar.
    /// </summary>
    public class NavBarGenerationPass : ICompilerPass
    {
        public void Execute(Page page)
        {
            string navbarHTML = NavBarUtil.ConvertNavBarToHTML(Program.Builder.NavBar);
            string compiledNavBar = HTMLUtil.ReplaceKeywordOutsideComments(
                HTMLTemplates.NavBarTemplate, "{{NAVBAR_CONTENT}}", navbarHTML);

            page.HTMLNavBarContents = compiledNavBar;
        }
    }

    /// <summary>
    /// Add Compiler Variables to the page.
    /// </summary>
    public class CompilerVariablePass : ICompilerPass
    {
        public void Execute(Page page)
        {
            string compiledHTML = HTMLUtil.ReplaceKeywordOutsideComments(
                HTMLTemplates.PageTemplate, "{{NAVBAR}}", page.HTMLNavBarContents);

            compiledHTML = HTMLUtil.ReplaceKeywordOutsideComments(
                compiledHTML, "{{HTML_DOCUMENTATION}}", page.HTMLDocumentationContents);

            compiledHTML = HTMLUtil.ReplaceKeywordOutsideComments(
                compiledHTML, "{{DOCUMENTATION_TITLE}}", page.Title);

            page.HTMLContents = compiledHTML;
        }
    }

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
}
