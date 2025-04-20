// Entry Point

using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DocumentationGenerator;
using DocumentationGenerator.Serializing;

namespace DocumentationGenerator
{
    class Program
    {
        /// <summary>
        /// The NavBar.
        /// </summary>
        public static NavBar NavBar { get; set; }
        static void Main(string[] args)
        {
            // Default output directory
            string outputPath = "Build";

            // Check if any arguments were passed
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: DocumentationGenerator.exe <JSON File>");
                return;
            }

            // Check for the '-o' argument
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-o" && i + 1 < args.Length)
                {
                    outputPath = args[i + 1];
                    i++;  // Skip the next argument as it's the value for -o
                }
            }

            // Get the directory of the JSON file to resolve relative paths
            string jsonFilePath = args[0];
            string jsonDirectory = Path.GetDirectoryName(jsonFilePath);

            // Parse the JSON into a NavBar
            string jsonContent = File.ReadAllText(jsonFilePath);
            NavBar = NavBarUtil.Deserialize(jsonContent);

            // Ensure output directory exists
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            foreach (NavTitle title in NavBar.Titles)
            {
                foreach (KeyValuePair<string, string> page in title.Pages)
                {
                    // Combine the JSON directory with the markdown file path
                    string markdownFilePath = Path.Combine(jsonDirectory, page.Value);

                    // Check if the markdown file exists
                    if (!File.Exists(markdownFilePath))
                    {
                        Console.WriteLine($"Markdown file not found: {markdownFilePath}");
                        continue;
                    }

                    // Create a new page
                    Page newPage = new Page();

                    // Set markdown contents
                    newPage.MarkdownContents = File.ReadAllText(markdownFilePath);

                    // Set the title
                    newPage.Title = page.Key;

                    // Get the path to the markdown file relative to the JSON directory
                    string relativeMarkdownPath = Path.GetRelativePath(jsonDirectory, markdownFilePath).Replace('\\', '/');

                    // Determine the full output path by combining the output directory and the relative markdown path
                    string fullOutputPath = Path.Combine(outputPath, relativeMarkdownPath);

                    // Ensure the directory for the relative markdown path exists
                    string outputDirectory = Path.GetDirectoryName(fullOutputPath);
                    if (!Directory.Exists(outputDirectory))
                    {
                        Directory.CreateDirectory(outputDirectory);
                    }

                    // Create the .html file path
                    fullOutputPath = Path.ChangeExtension(fullOutputPath, ".html");

                    // Write to a .html file
                    newPage.WriteToFile(fullOutputPath);

                    Console.WriteLine($"Generated: {fullOutputPath}");
                }
            }

            Console.WriteLine(NavBar.Titles[0].Title);
        }
    }
}
