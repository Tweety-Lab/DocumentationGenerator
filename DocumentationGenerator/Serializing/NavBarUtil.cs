using System.Text;
using System.Text.Json;

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
            var html = new StringBuilder();
            html.Append("<ul>");

            foreach (var title in navbar.Titles)
            {
                // Start title list item
                html.Append($"<li>{title.Title}");

                // Start nested pages list
                html.Append("<ul>");

                // Add pages as nested list items
                foreach (var page in title.Pages)
                {
                    string relativeLink = page.Value.Replace(".md", ".html");
                    // root-relative
                    html.Append($"<li><a href=\"/{relativeLink}\">{page.Key}</a></li>");
                }

                // Close nested pages list
                html.Append("</ul>");

                // Close title list item
                html.Append("</li>");
            }

            // Close main list
            html.Append("</ul>");
            return html.ToString();
        }
    }
}
