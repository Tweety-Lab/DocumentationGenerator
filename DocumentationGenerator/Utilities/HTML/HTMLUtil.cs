using System.Text.RegularExpressions;

namespace DocumentationGenerator.Utilities.HTML;

public static class HTMLUtil
{
    /// <summary>
    ///     Get all linked resource paths in a HTML file.
    /// </summary>
    /// <summary>
    ///     Get all linked resource paths (stylesheets, scripts, images) in an HTML file.
    /// </summary>
    public static List<string> GetLinkedHTMLResourcePaths(string html)
    {
        // Find all stylesheet links
        var styleSheetLinks = new List<string>();

        // Find all script links
        var scriptLinks = new List<string>();

        // Find all image sources
        var imageLinks = new List<string>();

        // Regex pattern to match <link rel="stylesheet" href="...">
        var styleSheetMatches = Regex.Matches(html, @"<link[^>]+rel\s*=\s*[""']stylesheet[""'][^>]+href\s*=\s*[""'](.*?)[""'][^>]*>", RegexOptions.IgnoreCase);

        // Regex pattern to match <script src="...">
        var scriptMatches = Regex.Matches(html, @"<script[^>]+src\s*=\s*[""'](.*?)[""'][^>]*>", RegexOptions.IgnoreCase);

        // Regex pattern to match <img src="...">
        var imageMatches = Regex.Matches(html, @"<img[^>]+src\s*=\s*[""'](.*?)[""'][^>]*>", RegexOptions.IgnoreCase);

        foreach (Match match in styleSheetMatches)
            styleSheetLinks.Add(match.Groups[1].Value);

        foreach (Match match in scriptMatches)
            scriptLinks.Add(match.Groups[1].Value);

        foreach (Match match in imageMatches)
            imageLinks.Add(match.Groups[1].Value);

        // Combine all resource paths
        return styleSheetLinks
            .Concat(scriptLinks)
            .Concat(imageLinks)
            .ToList();
    }

    /// <summary>
    ///     Replaces all occurrences of a keyword outside HTML comments.
    /// </summary>
    public static string ReplaceKeyword(string html, string keyword, string replacement)
    {
        // Split the HTML into parts outside and inside comments
        var parts = Regex.Split(html, @"(<!--.*?-->)", RegexOptions.Singleline);

        for (var i = 0; i < parts.Length; i++)
        {
            // Skip comment parts
            if (i % 2 != 0) continue;

            // Replace the keyword
            parts[i] = parts[i].Replace(keyword, replacement);
        }

        return string.Concat(parts);
    }

    /// <summary>
    ///     Replaces multiple keywords outside HTML comments.
    /// </summary>
    public static string ReplaceKeywords(string html, Dictionary<string, string> replacements)
    {
        var parts = Regex.Split(html, @"(<!--.*?-->)", RegexOptions.Singleline);

        for (var i = 0; i < parts.Length; i++)
        {
            if (i % 2 != 0) continue;

            foreach (var kvp in replacements) parts[i] = parts[i].Replace(kvp.Key, kvp.Value);
        }

        return string.Concat(parts);
    }
}