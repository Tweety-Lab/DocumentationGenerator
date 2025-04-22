using DocumentationGenerator.Builder;
using DocumentationGenerator.Utilities;
using DocumentationGenerator.MarkdownServer;

namespace DocumentationGenerator;

internal static class Program
{
    public static DocumentationBuilder? Builder { get; set; }

    private static void Main(string[] args)
    {
        try
        {
            var (mode, options) = CommandLineParser.ParseArguments(args);

            switch (mode)
            {
                case ApplicationMode.Host:
                    StartServer(options);
                    break;

                case ApplicationMode.Build:
                    BuildDocumentation(options);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }

    private static void StartServer(CommandLineOptions options)
    {
        Console.WriteLine($"Starting local server on port {options.HostPort.Value}...");
        var server = new MDServer("", options.HostPort.Value);
        server.OpenServer();
    }

    private static void BuildDocumentation(CommandLineOptions options)
    {
        if (options.JsonFilePath == null)
        {
            Console.WriteLine("Error: No JSON file path provided.");
            return;
        }

        // Builder
        Builder = new DocumentationBuilder(options.JsonFilePath, options.OutputPath);
        Builder.BuildAllPages();
    }
}