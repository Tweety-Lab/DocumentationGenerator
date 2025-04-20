using DocumentationGenerator.Markdown;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public void WriteToFile(string path)
        {
            // Ensure the directory exists
            string? directory = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Compile the page
            string finalHTML = CompilePage();

            File.WriteAllText(path, finalHTML);
        }

        private string CompilePage()
        {
            // Convert Markdown to HTML
            string bodyHTML = MarkdownUtil.ConvertMarkdownToHTML(MarkdownContents);

            string navbarHTML = NavBarUtil.ConvertNavBarToHTML(Program.NavBar);

            // Compile the NavBar HTML into a proper static page
            navbarHTML = HTMLTemplates.NavBarTemplate
                .Replace("{{NAVBAR_CONTENT}}", navbarHTML);

            // Compile the HTML into a proper static page
            string finalHTML = HTMLTemplates.PageTemplate
                .Replace("{{HTML_DOCUMENTATION}}", bodyHTML)
                .Replace("{{DOCUMENTATION_TITLE}}", Title)
                .Replace("{{NAVBAR}}", navbarHTML);

            return finalHTML;
        }
    }
}
