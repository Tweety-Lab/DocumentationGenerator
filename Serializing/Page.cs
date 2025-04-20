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

            // Final compilation pass to remove all comments
            finalHTML = Regex.Replace(finalHTML, "<!--.*?-->", "", RegexOptions.Singleline);

            return finalHTML;
        }

        /// <summary>
        /// Get all linked resource paths in the markdown
        /// </summary>
        /// <returns></returns>
        public List<string> GetLinkedResourcePaths()
        {
            var paths = new List<string>();

            // Regex pattern to match markdown links: [text](path "optional title")
            var linkMatches = Regex.Matches(MarkdownContents, @"\[.*?\]\((.*?)(?:\s+[""'].*?[""'])?\)");

            foreach (Match match in linkMatches)
            {
                if (match.Groups.Count < 2) continue;

                string rawPath = match.Groups[1].Value.Trim();

                // Skip empty paths, anchors (#), and external URLs
                if (string.IsNullOrWhiteSpace(rawPath) ||
                    rawPath.StartsWith("#") ||
                    rawPath.StartsWith("http://") ||
                    rawPath.StartsWith("https://") ||
                    rawPath.StartsWith("mailto:"))
                {
                    continue;
                }

                // Remove query strings and anchors from the path
                var cleanPath = rawPath.Split(new[] { '#', '?' }, 2)[0];

                if (!string.IsNullOrWhiteSpace(cleanPath))
                {
                    paths.Add(cleanPath);
                }
            }

            return paths.Distinct().ToList(); // Return unique paths
        }
    }
}
