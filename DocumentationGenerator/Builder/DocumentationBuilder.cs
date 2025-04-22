using DocumentationGenerator.Utilities.HTML;
using DocumentationGenerator.Utilities.Markdown;
using DocumentationGenerator.Serializing;
using DocumentationGenerator.Utilities;
using DocumentationGenerator.Utilities.NavBar;
using DocumentationGenerator.Utilities.Themes;

namespace DocumentationGenerator.Builder;

public class DocumentationBuilder
{
    public DocumentationBuilder(string jsonFilePath, string outputPath)
    {
        OutputPath = outputPath;
        JsonDirectory = Path.GetDirectoryName(Path.GetFullPath(jsonFilePath));

        // Load and deserialize navbar
        var jsonContent = File.ReadAllText(jsonFilePath);
        NavBar = NavBarUtil.Deserialize(jsonContent);

        // Load and deserialize config
        var configPath = Path.Combine(JsonDirectory, "config.json");
        var configJson = File.ReadAllText(configPath);
        var config = System.Text.Json.JsonSerializer.Deserialize<Config>(configJson);

        HTMLTemplates.Theme = ThemeUtil.LoadTheme(config.Theme);

        // Ensure output directory exists
        Directory.CreateDirectory(OutputPath);
    }

    public NavBar NavBar { get; }
    public string OutputPath { get; }
    public string JsonDirectory { get; }

    public void BuildAllPages()
    {
        foreach (var title in NavBar.Titles)
        foreach (var page in title.Pages)
            ProcessPage(page);

        Console.WriteLine("Documentation generation complete!");
    }

    private void ProcessPage(KeyValuePair<string, string> page)
    {
        var markdownFilePath = Path.Combine(JsonDirectory, page.Value);

        if (!File.Exists(markdownFilePath))
        {
            Console.WriteLine($"Warning: Markdown file not found: {markdownFilePath}");
            return;
        }

        var newPage = new Page
        {
            MarkdownContents = FileUtil.SafeReadAllText(markdownFilePath),
            Title = page.Key
        };

        var relativeMarkdownPath = Path.GetRelativePath(JsonDirectory, markdownFilePath);
        var fullOutputPath = Path.Combine(OutputPath, Path.ChangeExtension(relativeMarkdownPath, ".html"));

        // Ensure output directory exists
        Directory.CreateDirectory(Path.GetDirectoryName(fullOutputPath));

        // Write HTML file
        newPage.WriteToFile(fullOutputPath);

        // Copy resources requested by markdown (imgs, etc)
        foreach (var resourcePath in MarkdownUtil.GetLinkedMDResourcePaths(newPage.MarkdownContents))
            CopyResource(resourcePath, JsonDirectory, Path.GetDirectoryName(fullOutputPath));

        // Copy resources requested by HTML (css, js, etc)
        foreach (var resourcePath in HTMLUtil.GetLinkedHTMLResourcePaths(newPage.HTMLContents))
            CopyResource(resourcePath, HTMLTemplates.Theme.Paths.Root, Path.GetDirectoryName(fullOutputPath));

        Console.WriteLine($"Generated: {fullOutputPath}");
    }

    private void CopyResource(string resourcePath, string sourceBaseDir, string destinationDir)
    {
        try
        {
            var fullSourcePath = Path.Combine(sourceBaseDir, resourcePath);
            if (!File.Exists(fullSourcePath))
            {
                Console.WriteLine($"Warning: Resource not found: {resourcePath}");
                return;
            }

            var fullDestPath = Path.Combine(destinationDir, resourcePath);
            Directory.CreateDirectory(Path.GetDirectoryName(fullDestPath));
            File.Copy(fullSourcePath, fullDestPath, true);
            Console.WriteLine($"Copied resource: {resourcePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error copying resource {resourcePath}: {ex.Message}");
        }
    }
}