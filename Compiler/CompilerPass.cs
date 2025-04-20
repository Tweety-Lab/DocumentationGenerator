using DocumentationGenerator.Serializing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentationGenerator.Compilation
{
    // Base Compiler Pass Interface
    public interface ICompilerPass
    {
        void Execute(Page page);
    }
}
