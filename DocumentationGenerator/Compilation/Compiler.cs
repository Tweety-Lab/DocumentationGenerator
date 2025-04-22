using DocumentationGenerator.Compilation.Passes;
using DocumentationGenerator.Serializing;

namespace DocumentationGenerator.Compilation
{
    public static class Compiler
    {
        // All compiler passes to run
        public static readonly List<ICompilerPass> CompilerPasses =
        [
            new MarkdownConversionPass(),
            new NavBarGenerationPass(),
            new CompilerVariablePass(),
            new CommentRemovalPass()
        ];

        /// <summary>
        /// Compiles a single page.
        /// </summary>
        /// <param name="page"></param>
        public static void CompilePage(Page page)
        {
            foreach (ICompilerPass pass in CompilerPasses)
            {
                pass.Execute(page);
            }
        }

        /// <summary>
        /// Compiles a list of pages.
        /// </summary>
        /// <param name="pages"></param>
        public static void CompilePages(List<Page> pages)
        {
            foreach (Page page in pages)
            {
                CompilePage(page);
            }
        }
    }
}
