using DocumentationGenerator.HTML;
using DocumentationGenerator.Markdown;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DocumentationGenerator.Serializing
{
    /// <summary>
    /// Markdown Page
    /// </summary>
    public class Page
    {
        public string Title { get; set; }
        public string MarkdownContents { get; set; }
        public string HTMLContents { get; set; }

        public void WriteToFile(string path)
        {
            // Ensure the directory exists
            string? directory = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (HTMLContents == null)
                CompilePage();

            File.WriteAllText(path, HTMLContents);
        }

        /// <summary>
        /// Compile the page into HTML.
        /// </summary>
        public void CompilePage()
        {
            // Convert Markdown to HTML
            string bodyHTML = MarkdownUtil.ConvertMarkdownToHTML(MarkdownContents);
            string navbarHTML = NavBarUtil.ConvertNavBarToHTML(Program.NavBar);

            // Compile the NavBar HTML into a proper static page
            string compiledNavBar = HTMLUtil.ReplaceKeywordOutsideComments(
                HTMLTemplates.NavBarTemplate, "{{NAVBAR_CONTENT}}", navbarHTML);

            // Prepare the template with NavBar
            string compiledHTML = HTMLUtil.ReplaceKeywordOutsideComments(
                HTMLTemplates.PageTemplate, "{{NAVBAR}}", compiledNavBar);

            // Replace the rest of the placeholders outside of HTML comments
            // We remove comments in a later pass anyway but we do this for safety
            compiledHTML = HTMLUtil.ReplaceKeywordOutsideComments(compiledHTML, "{{HTML_DOCUMENTATION}}", bodyHTML);
            compiledHTML = HTMLUtil.ReplaceKeywordOutsideComments(compiledHTML, "{{DOCUMENTATION_TITLE}}", Title);

            // Final compilation pass to remove all comments from HTML content
            compiledHTML = Regex.Replace(compiledHTML, "<!--.*?-->", "", RegexOptions.Singleline);

            HTMLContents = compiledHTML;
        }
    }
}
