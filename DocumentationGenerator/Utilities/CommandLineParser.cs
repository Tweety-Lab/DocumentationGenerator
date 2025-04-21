namespace DocumentationGenerator.FileUtilities;

public class CommandLineOptions
{
    public string? JsonFilePath { get; set; }
    public string OutputPath { get; set; } = "Build";
    public int? HostPort { get; set; }
}

public static class CommandLineParser
{
    public static CommandLineOptions ParseArguments(string[] args)
    {
        var options = new CommandLineOptions();

        if (args.Length == 0)
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("  DocumentationGenerator.exe <Markdown Directory> [-o OutputDirectory]");
            Console.WriteLine("  DocumentationGenerator.exe host [Port]");
            throw new ArgumentException("No arguments provided");
        }

        if (args[0] == "host")
        {
            // Hosting mode
            if (args.Length > 1 && int.TryParse(args[1], out var port))
                options.HostPort = port;
            else
                options.HostPort = 9999; // default port
        }
        else
        {
            // Build mode
            options.JsonFilePath = args[0] + "/navbar.json";

            for (var i = 1; i < args.Length; i++)
                if (args[i] == "-o" && i + 1 < args.Length)
                {
                    options.OutputPath = args[i + 1];
                    i++;
                }
        }

        return options;
    }
}