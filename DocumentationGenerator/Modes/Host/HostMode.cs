using ApplicationModes;
using DocumentationGenerator.MarkdownServer;
using DocumentationGenerator.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentationGenerator.Modes.Host
{
    public class HostMode : IApplicationMode
    {
        public void Start(CommandLineOptions options)
        {
            Console.WriteLine($"Starting local server on port {options.HostPort.Value}...");
            var server = new MDServer("", options.HostPort.Value);
            server.OpenServer();
        }
    }
}
