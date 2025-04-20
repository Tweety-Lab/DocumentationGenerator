using DocumentationGenerator.Serializing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DocumentationGenerator
{
    class Program
    {
        public static NavBar NavBar { get; set; }

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: DocumentationGenerator.exe <JSON File> [-o OutputDirectory]");
                return;
            }

            string outputPath = "Build";
            string jsonFilePath = args[0];
            string jsonDirectory = Path.GetDirectoryName(Path.GetFullPath(jsonFilePath));

            // Parse command line arguments
            for (int i = 1; i < args.Length; i++)
            {
                if (args[i] == "-o" && i + 1 < args.Length)
                {
                    outputPath = args[i + 1];
                    i++;
                }
            }

            // Load and deserialize navbar
            string jsonContent = File.ReadAllText(jsonFilePath);
            NavBar = NavBarUtil.Deserialize(jsonContent);

            // Ensure output directory exists
            Directory.CreateDirectory(outputPath);

            // Process all pages
            foreach (NavTitle title in NavBar.Titles)
            {
                foreach (KeyValuePair<string, string> page in title.Pages)
                {
                    ProcessPage(page, jsonDirectory, outputPath);
                }
            }

            Console.WriteLine("Documentation generation complete!");
        }

        /// <summary>
        /// Processes a single page.
        /// </summary>
        static void ProcessPage(KeyValuePair<string, string> page, string jsonDirectory, string outputPath)
        {
            string markdownFilePath = Path.Combine(jsonDirectory, page.Value);

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

            string relativeMarkdownPath = Path.GetRelativePath(jsonDirectory, markdownFilePath);
            string fullOutputPath = Path.Combine(outputPath, Path.ChangeExtension(relativeMarkdownPath, ".html"));

            // Ensure output directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(fullOutputPath));

            // Write HTML file
            newPage.WriteToFile(fullOutputPath);

            // Copy linked resources
            foreach (string resourcePath in newPage.GetLinkedResourcePaths())
            {
                CopyResource(resourcePath, jsonDirectory, Path.GetDirectoryName(fullOutputPath));
            }

            Console.WriteLine($"Generated: {fullOutputPath}");
        }

        /// <summary>
        /// Copies a resource from the source directory to the destination directory.
        /// </summary>
        static void CopyResource(string resourcePath, string sourceBaseDir, string destinationDir)
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
