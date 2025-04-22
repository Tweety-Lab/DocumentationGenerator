using DocumentationGenerator.Builder;
using DocumentationGenerator.Utilities;
using DocumentationGenerator.MarkdownServer;
using DocumentationGenerator.Serializing;
using DocumentationGenerator.Modes.Host;
using DocumentationGenerator.Modes.Build;
using DocumentationGenerator.Modes;

namespace DocumentationGenerator;

internal static class Program
{
    public static DocumentationBuilder? Builder { get; set; }

    private static void Main(string[] args)
    {
        try
        {
            var (mode, options) = CommandLineParser.ParseArguments(args);
            ModeRegistry.GetMode(mode).Start(options);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }
}