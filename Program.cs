// Entry Point

using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DocumentationGenerator;
using DocumentationGenerator.JSON;

namespace DocumentationGenerator
{
    class Program
    {
        static void Main(string[] args)
        {

            List<NavTitle> navTitles = new List<NavTitle>()
            {
                new NavTitle()
                {
                    Pages = new Dictionary<string, string>()
                    {
                        {"THIS IS A BUTTON", "this is the path to the markdown file linked with the button" }
                    }
                }
            };

            NavBar navbar = new NavBar()
            {
                Titles = navTitles
            };


            // Write json to file
            File.WriteAllText("../../../JSON_TEMPLATE.json", JsonSerializer.Serialize(navbar));
        }
    }
}