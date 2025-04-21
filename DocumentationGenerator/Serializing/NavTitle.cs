namespace DocumentationGenerator.Serializing;

public struct NavTitle
{
    // Name of the title
    public string Title { get; set; }

    // Button Name - Page Path
    public Dictionary<string, string> Pages { get; set; }
}