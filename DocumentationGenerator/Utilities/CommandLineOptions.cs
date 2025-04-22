namespace DocumentationGenerator.Utilities;

public class CommandLineOptions
{
    public string? JsonFilePath { get; set; }
    public string OutputPath { get; set; } = "Build";
    public int? HostPort { get; set; }
    public string? ThemePath { get; set; }
}