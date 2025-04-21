using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentationGenerator.Utilities
{
    public static class CommandLineParser
    {
        public static (string jsonFilePath, string outputPath) ParseArguments(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: DocumentationGenerator.exe <JSON File> [-o OutputDirectory]");
                throw new ArgumentException("No arguments provided");
            }

            string outputPath = "Build";
            string jsonFilePath = args[0];

            // Parse command line arguments
            for (int i = 1; i < args.Length; i++)
            {
                if (args[i] == "-o" && i + 1 < args.Length)
                {
                    outputPath = args[i + 1];
                    i++;
                }
            }

            return (jsonFilePath, outputPath);
        }
    }
}
