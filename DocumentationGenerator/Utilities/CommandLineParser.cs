using static DocumentationGenerator.Program;

namespace DocumentationGenerator.Utilities;

// Application mode (host, build, etc)
public enum ApplicationMode
{
    Host,
    Build
}

public static class CommandLineParser
{
    public static (ApplicationMode mode, CommandLineOptions options) ParseArguments(string[] args)
    {
        if (args.Length == 0)
        {
            PrintUsage();
            throw new ArgumentException("No arguments provided");
        }

        if (!Enum.TryParse<ApplicationMode>(args[0], true, out var mode))
        {
            Console.WriteLine($"Unknown mode: {args[0]}");
            PrintUsage();
            throw new ArgumentException($"Invalid mode: {args[0]}");
        }

        var modeArgs = args.Skip(1).ToArray();
        var options = ParseModeOptions(mode, modeArgs);

        return (mode, options);
    }

    private static CommandLineOptions ParseModeOptions(ApplicationMode mode, string[] args)
    {
        var options = new CommandLineOptions();

        switch (mode)
        {
            case ApplicationMode.Host:
                options.HostPort = (args.Length > 0 && int.TryParse(args[0], out var port))
                    ? port
                    : 9999;
                break;

            case ApplicationMode.Build:
                if (args.Length == 0)
                {
                    Console.WriteLine("Missing markdown directory path.");
                    PrintUsage();
                    throw new ArgumentException("Missing markdown directory path.");
                }

                options.JsonFilePath = args[0] + "/navbar.json";

                for (var i = 1; i < args.Length; i++)
                {
                    if (args[i] == "-o" && i + 1 < args.Length)
                    {
                        options.OutputPath = args[i + 1];
                        i++;
                    }
                }
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(mode), "Unhandled application mode");
        }

        return options;
    }

    private static void PrintUsage()
    {
        Console.WriteLine("Usage:");
        Console.WriteLine("  DocumentationGenerator.exe Host [Port]");
        Console.WriteLine("  DocumentationGenerator.exe Build <Markdown Directory> [-o OutputDirectory]");
    }
}