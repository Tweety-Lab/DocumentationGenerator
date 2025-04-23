using System.Text;
using System.Text.Json;

namespace DocumentationGenerator.Utilities.NavBar;

public static class NavBarUtil
{
    /// <summary>
    ///     Convert JSON into a NavBar object.
    /// </summary>
    /// <param name="json"></param>
    /// <returns>Deserialized object</returns>
    public static Serializing.NavBar Deserialize(string json)
    {
        return JsonSerializer.Deserialize<Serializing.NavBar>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }

    /// <summary>
    /// Convert a NavBar object to HTML
    /// </summary>
    public static string ConvertNavBarToHTML(Serializing.NavBar navbar)
    {
        var html = new StringBuilder();
        html.Append("<ul class=\"list nav__main-list collapsible__content\">");

        foreach (var title in navbar.Titles)
        {
            // Start title list item
            html.Append($"<li class=\"nav__title-item\">{title.Title}");

            // Start nested pages list
            html.Append("<ul class=\"list\">");

            // Add pages as nested list items
            foreach (var page in title.Pages)
            {
                var relativeLink = page.Value.Replace(".md", ".html");
                // root-relative
                html.Append($"<li class=\"nav__list-item\"><a href=\"/{relativeLink}\">{page.Key}</a></li>");
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