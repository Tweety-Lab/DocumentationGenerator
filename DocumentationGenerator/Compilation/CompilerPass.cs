using DocumentationGenerator.Serializing;

namespace DocumentationGenerator.Compilation
{
    // Base Compiler Pass Interface
    public interface ICompilerPass
    {
        void Execute(Page page);
    }
}
