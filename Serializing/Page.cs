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

            File.WriteAllText(path, MarkdownUtil.ConvertMarkdownToHtml(MarkdownContents));
        }
    }
}
