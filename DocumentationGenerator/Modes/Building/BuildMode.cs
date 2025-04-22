using ApplicationModes;
using DocumentationGenerator.Builder;
using DocumentationGenerator.MarkdownServer;
using DocumentationGenerator.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentationGenerator.Modes.Build
{
    public class BuildMode : IApplicationMode
    {
        public void Start(CommandLineOptions options)
        {
            if (options.JsonFilePath == null)
            {
                Console.WriteLine("Error: No JSON file path provided.");
                return;
            }

            // Builder
            Program.Builder = new DocumentationBuilder(options.JsonFilePath, options.OutputPath);
            Program.Builder.BuildAllPages();
        }
    }
}
