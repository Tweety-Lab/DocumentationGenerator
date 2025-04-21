using DocumentationGenerator.Serializing;

namespace DocumentationGenerator.Compilation.Passes;

// Base Compiler Pass Interface
public interface ICompilerPass
{
    void Execute(Page page);
}