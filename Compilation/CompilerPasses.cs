using DocumentationGenerator.FileUtilities.HTML;
using DocumentationGenerator.FileUtilities.Markdown;
using DocumentationGenerator.Serializing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DocumentationGenerator.Compilation
{
    /// <summary>
    /// Converts Markdown to HTML.
    /// </summary>
    public class MarkdownConversionPass : ICompilerPass
    {
        public void Execute(Page page)
        {
            // Convert Markdown to HTML
            page.HTMLContents = MarkdownUtil.ConvertMarkdownToHTML(page.MarkdownContents);
        }
    }

    /// <summary>
    /// Compiles the Navigation Bar.
    /// </summary>
    public class NavBarGenerationPass : ICompilerPass
    {
        public void Execute(Page page)
        {
            string navbarHTML = NavBarUtil.ConvertNavBarToHTML(Program.NavBar);
            string compiledNavBar = HTMLUtil.ReplaceKeywordOutsideComments(
                HTMLTemplates.NavBarTemplate, "{{NAVBAR_CONTENT}}", navbarHTML);

            page.HTMLContents = HTMLUtil.ReplaceKeywordOutsideComments(
                page.HTMLContents, "{{NAVBAR}}", compiledNavBar);
        }
    }

    /// <summary>
    /// Add Compiler Variables to the page.
    /// </summary>
    public class TemplateProcessingPass : ICompilerPass
    {
        public void Execute(Page page)
        {
            // Prepare the template with NavBar
            string compiledHTML = HTMLUtil.ReplaceKeywordOutsideComments(
                HTMLTemplates.PageTemplate, "{{NAVBAR}}", page.HTMLContents);

            // Replace the rest of the placeholders
            compiledHTML = HTMLUtil.ReplaceKeywordOutsideComments(
                compiledHTML, "{{HTML_DOCUMENTATION}}", page.HTMLContents);
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
