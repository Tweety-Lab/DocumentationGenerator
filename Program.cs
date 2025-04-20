using DocumentationGenerator.Builder;
using DocumentationGenerator.FileUtilities.HTML;
using DocumentationGenerator.FileUtilities.Markdown;
using DocumentationGenerator.Serializing;
using DocumentationGenerator.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DocumentationGenerator
{
    namespace DocumentationGenerator
    {
        static class Program
        {
            // Builder
            public static DocumentationBuilder Builder { get; set; }

            static void Main(string[] args)
            {
                try
                {
                    var (jsonFilePath, outputPath) = CommandLineParser.ParseArguments(args);

                    Builder = new DocumentationBuilder(jsonFilePath, outputPath);
                    Builder.BuildAllPages();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
