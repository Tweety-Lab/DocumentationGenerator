using System.Text.RegularExpressions;
using Markdig;

namespace DocumentationGenerator.Utilities.Markdown;

public static class MarkdownUtil
{
    public static string ConvertMarkdownToHTML(string markdown)
    {
        // Create a Markdown pipeline
        var pipeline = new MarkdownPipelineBuilder()
    .UseAdvancedExtensions()
    .Build();

        // Parse with the pipeline
        var document = Markdig.Markdown.Parse(markdown, pipeline);

        // Convert to HTML
        var html = document.ToHtml(pipeline);

        // Return HTML
        return html;
    }

    /// <summary>
    ///     Get all linked resource paths in a markdown file.
    /// </summary>
    public static List<string> GetLinkedMDResourcePaths(string markdown)
    {
        var paths = new List<string>();

        // Regex pattern to match markdown links: [text](path "optional title")
        var linkMatches = Regex.Matches(markdown, @"\[.*?\]\((.*?)(?:\s+[""'].*?[""'])?\)");

        foreach (Match match in linkMatches)
        {
            if (match.Groups.Count < 2) continue;

            var rawPath = match.Groups[1].Value.Trim();

            // Skip empty paths, anchors (#), and external URLs
            if (string.IsNullOrWhiteSpace(rawPath) ||
                rawPath.StartsWith("#") ||
                rawPath.StartsWith("http://") ||
                rawPath.StartsWith("https://") ||
                rawPath.StartsWith("mailto:"))
                continue;

            // Remove query strings and anchors from the path
            var cleanPath = rawPath.Split(new[] { '#', '?' }, 2)[0];

            if (!string.IsNullOrWhiteSpace(cleanPath)) paths.Add(cleanPath);
        }

        return paths.Distinct().ToList(); // Return unique paths
    }
}