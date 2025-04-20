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
        public string MarkdownContents { get; set; }

        public void WriteToFile(string path)
        {
            // Ensure the directory exists
            string? directory = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            
            // Convert Markdown to HTML
            string HTML = MarkdownUtil.ConvertMarkdownToHtml(MarkdownContents);

            // Load the HTML into the page template
            HTML = HTMLTemplates.PageTemplate.Replace("{{HTML_DOCUMENTATION}}", HTML);

            File.WriteAllText(path, HTML);
        }
    }
}
