using DocumentationGenerator.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationModes;

// Base Applictaion Mode
public interface IApplicationMode
{
    void Start(CommandLineOptions options);
}
