using DocumentationGenerator.Compilation;

namespace DocumentationGenerator.Serializing
{
    /// <summary>
    /// Markdown Page
    /// </summary>
    public class Page
    {
        // All compiler passes to run
        private static readonly List<ICompilerPass> _compilerPasses = new List<ICompilerPass>
        {
            new MarkdownConversionPass(),
            new NavBarGenerationPass(),
            new CompilerVariablePass(),
            new CommentRemovalPass()
        };

        public string Title { get; set; }
        public string MarkdownContents { get; set; }

        // HTML Content
        public string HTMLDocumentationContents { get; set; }
        public string HTMLNavBarContents { get; set; }
        public string HTMLContents { get; set; }

        public void WriteToFile(string path)
        {
            string? directory = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (HTMLContents == null)
                CompilePage();

            File.WriteAllText(path, HTMLContents);
        }


        /// <summary>
        /// Compile the page.
        /// </summary>
        public void CompilePage()
        {
            HTMLDocumentationContents ??= MarkdownContents;

            // Apply compiler passes
            foreach (var pass in _compilerPasses)
            {
                pass.Execute(this);
            }
        }

        public static void RegisterCompilerPass(ICompilerPass pass)
        {
            _compilerPasses.Add(pass);
        }
    }
}