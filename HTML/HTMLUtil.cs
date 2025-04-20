using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DocumentationGenerator.HTML
{
    public static class HTMLUtil
    {
        /// <summary>
        /// Get all linked resource paths in a HTML file.
        /// </summary>
        public static List<string> GetLinkedHTMLResourcePaths(string html)
        {
            // Find all style sheet links in the HTML file
            var styleSheetLinks = new List<string>();

            // Find all script links in the HTML file
            var scriptLinks = new List<string>();

            // Regex pattern to match style sheet links: <link rel="stylesheet" href="path" />
            var styleSheetMatches = Regex.Matches(html, @"<link[^>]+rel=""stylesheet""[^>]+href=""(.*?)""[^>]*>");

            // Regex pattern to match script links: <script src="path"></script>
            var scriptMatches = Regex.Matches(html, @"<script[^>]+src=""(.*?)""[^>]*>");

            foreach (Match match in styleSheetMatches)
            {
                styleSheetLinks.Add(match.Groups[1].Value);
            }

            foreach (Match match in scriptMatches)
            {
                scriptLinks.Add(match.Groups[1].Value);
            }

            return styleSheetLinks.Concat(scriptLinks).ToList();
        }
    }
}
