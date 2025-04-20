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
            string finalHTML = CompilePage(Title, MarkdownContents);

            File.WriteAllText(path, finalHTML);
        }

        private string CompilePage(string title, string markdownContents)
        {
            // Convert Markdown to HTML
            string bodyHTML = MarkdownUtil.ConvertMarkdownToHtml(MarkdownContents);

            string navbarHTML = HTMLTemplates.NavbarTemplate;

            // Compile the HTML into a proper static page
            string finalHTML = HTMLTemplates.PageTemplate
                .Replace("{{HTML_DOCUMENTATION}}", bodyHTML)
                .Replace("{{DOCUMENTATION_TITLE}}", Title)
                .Replace("{{NAVBAR}}", navbarHTML);

            return finalHTML;
        }
    }
}
