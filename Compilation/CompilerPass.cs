using DocumentationGenerator.Serializing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentationGenerator.Compilation
{
    public interface ICompilerPass
    {
        void Execute(Page page);
    }
}
