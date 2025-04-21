using DocumentationGenerator.Builder;
using DocumentationGenerator.FileUtilities;
using DocumentationGenerator.MarkdownServer;

namespace DocumentationGenerator;

internal static class Program
{
    public static DocumentationBuilder? Builder { get; set; }

    private static void Main(string[] args)
    {
        try
        {
            var options = CommandLineParser.ParseArguments(args);

            // Local server
            if (options.HostPort.HasValue)
            {
                Console.WriteLine($"Starting local server on port {options.HostPort.Value}...");
                var server = new MDServer("", options.HostPort.Value);
                server.OpenServer();

                return;
            }

            if (options.JsonFilePath == null)
            {
                Console.WriteLine("Error: No JSON file path provided.");
                return;
            }

            // Builder
            Builder = new DocumentationBuilder(options.JsonFilePath, options.OutputPath);
            Builder.BuildAllPages();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}