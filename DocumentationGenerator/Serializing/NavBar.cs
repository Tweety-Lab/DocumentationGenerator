namespace DocumentationGenerator.Serializing
{
    public class NavBar
    {
        // List of NavTitles
        public List<NavTitle> Titles { get; set; } = new();
    }

    public struct NavTitle
    {
        // Name of the title
        public string Title { get; set; }

        // Button Name - Page Path
        public Dictionary<string, string> Pages { get; set; }
    }
}
