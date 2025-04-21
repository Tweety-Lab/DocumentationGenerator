using DocumentationGenerator.Serializing;

namespace DocumentationGenerator.Compilation
{
    public static class Compiler
    {
        /// <summary>
        /// Compiles a single page.
        /// </summary>
        /// <param name="page"></param>
        public static void CompilePage(Page page)
        {
            foreach (ICompilerPass pass in Page.CompilerPasses)
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
