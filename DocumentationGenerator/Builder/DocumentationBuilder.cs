using DocumentationGenerator.FileUtilities.HTML;
using DocumentationGenerator.FileUtilities.Markdown;
using DocumentationGenerator.Serializing;

namespace DocumentationGenerator.Builder
{
    public class DocumentationBuilder
    {
        public NavBar NavBar { get; private set; }
        public string OutputPath { get; private set; }
        public string JsonDirectory { get; private set; }

        public DocumentationBuilder(string jsonFilePath, string outputPath)
        {
            OutputPath = outputPath;
            JsonDirectory = Path.GetDirectoryName(Path.GetFullPath(jsonFilePath));

            // Load and deserialize navbar
            string jsonContent = File.ReadAllText(jsonFilePath);
            NavBar = NavBarUtil.Deserialize(jsonContent);

            // Ensure output directory exists
            Directory.CreateDirectory(OutputPath);
        }

        public void BuildAllPages()
        {
            foreach (NavTitle title in NavBar.Titles)
            {
                foreach (KeyValuePair<string, string> page in title.Pages)
                {
                    ProcessPage(page);
                }
            }

            Console.WriteLine("Documentation generation complete!");
        }

        private void ProcessPage(KeyValuePair<string, string> page)
        {
            string markdownFilePath = Path.Combine(JsonDirectory, page.Value);

            if (!File.Exists(markdownFilePath))
            {
                Console.WriteLine($"Warning: Markdown file not found: {markdownFilePath}");
                return;
            }

            var newPage = new Page
            {
                MarkdownContents = File.ReadAllText(markdownFilePath),
                Title = page.Key
            };

            string relativeMarkdownPath = Path.GetRelativePath(JsonDirectory, markdownFilePath);
            string fullOutputPath = Path.Combine(OutputPath, Path.ChangeExtension(relativeMarkdownPath, ".html"));

            // Ensure output directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(fullOutputPath));

            // Write HTML file
            newPage.WriteToFile(fullOutputPath);

            // Copy resources requested by markdown (imgs, etc)
            foreach (string resourcePath in MarkdownUtil.GetLinkedMDResourcePaths(newPage.MarkdownContents))
            {
                CopyResource(resourcePath, JsonDirectory, Path.GetDirectoryName(fullOutputPath));
            }

            // Copy resources requested by HTML (css, js, etc)
            foreach (string resourcePath in HTMLUtil.GetLinkedHTMLResourcePaths(newPage.HTMLContents))
            {
                CopyResource(resourcePath, HTMLTemplates.TemplatePath, Path.GetDirectoryName(fullOutputPath));
            }

            Console.WriteLine($"Generated: {fullOutputPath}");
        }

        private void CopyResource(string resourcePath, string sourceBaseDir, string destinationDir)
        {
            try
            {
                string fullSourcePath = Path.Combine(sourceBaseDir, resourcePath);
                if (!File.Exists(fullSourcePath))
                {
                    Console.WriteLine($"Warning: Resource not found: {resourcePath}");
                    return;
                }

                string fullDestPath = Path.Combine(destinationDir, resourcePath);
                Directory.CreateDirectory(Path.GetDirectoryName(fullDestPath));
                File.Copy(fullSourcePath, fullDestPath, overwrite: true);
                Console.WriteLine($"Copied resource: {resourcePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error copying resource {resourcePath}: {ex.Message}");
            }
        }
    }
}