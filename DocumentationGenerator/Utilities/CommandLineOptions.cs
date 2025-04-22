namespace DocumentationGenerator.Utilities;

public class CommandLineOptions
{
    public string? Directory { get; set; }
    public string OutputPath { get; set; } = "Build";
    public int? HostPort { get; set; }
}