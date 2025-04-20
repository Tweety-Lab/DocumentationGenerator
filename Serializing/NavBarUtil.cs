using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DocumentationGenerator.Serializing
{
    public static class NavBarUtil
    {
        /// <summary>
        /// Convert JSON into a NavBar object.
        /// </summary>
        /// <param name="json"></param>
        /// <returns>Deserialized object</returns>
        public static NavBar Deserialize(string json)
        {
            return JsonSerializer.Deserialize<NavBar>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        /// <summary>
        /// Convert a NavBar object to HTML
        /// </summary>
        /// <param name="navbar"></param>
        /// <returns></returns>
        public static string ConvertNavBarToHTML(NavBar navbar)
        {
            string html = string.Empty;
            foreach (var title in navbar.Titles)
            {
                // Add the Title
                html += $"<h3>{title.Title}</h3>";

                // Add pages
                foreach (var page in title.Pages)
                {
                    string relativeLink = page.Value.Replace(".md", ".html");
                    html += $"<p><a href=\"{relativeLink}\">{page.Key}</a></p>";
                }
            }
            return html;
        }
    }
}
