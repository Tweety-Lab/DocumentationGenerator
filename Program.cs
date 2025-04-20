// Entry Point

using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DocumentationGenerator;
using DocumentationGenerator.Serializing;

namespace DocumentationGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Check if any arguments were passed
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: DocumentationGenerator.exe <JSON File>");
                return;
            }

            // Parse the JSON into a NavBar
            string jsonContent = File.ReadAllText(args[0]);
            NavBar navbar = NavBarUtil.Deserialize(jsonContent);

            Console.WriteLine(navbar.Titles[0].Title);
        }
    }
}