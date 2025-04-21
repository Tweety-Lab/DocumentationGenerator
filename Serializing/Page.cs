using DocumentationGenerator.Compilation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
        public string HTMLContents { get; set; }

        public void WriteToFile(string path)
        {
            // Ensure the directory exists
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
            // Initialize HTML contents with markdown
            HTMLContents ??= MarkdownContents;

            // Apply compiler passes
            foreach (var pass in _compilerPasses)
            {
                pass.Execute(this);
            }
        }

        // Allow registering compiler passes
        public static void RegisterCompilerPass(ICompilerPass pass)
        {
            _compilerPasses.Add(pass);
        }
    }
}