using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Markdig;
using Markdig.Syntax;

namespace DocumentationGenerator.Markdown
{
    public static class MarkdownUtil
    {
        public static string ConvertMarkdownToHtml(string markdown)
        {
            // Parse to markdown
            MarkdownDocument document = Markdig.Markdown.Parse(markdown);

            // Convert to HTML
            var html = document.ToHtml();

            // Return HTML
            return html;
        }
    }
}
