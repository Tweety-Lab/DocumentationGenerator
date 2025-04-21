using System.Text.RegularExpressions;

namespace DocumentationGenerator.FileUtilities.HTML;

public static class HTMLUtil
{
    /// <summary>
    ///     Get all linked resource paths in a HTML file.
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

        foreach (Match match in styleSheetMatches) styleSheetLinks.Add(match.Groups[1].Value);

        foreach (Match match in scriptMatches) scriptLinks.Add(match.Groups[1].Value);

        return styleSheetLinks.Concat(scriptLinks).ToList();
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