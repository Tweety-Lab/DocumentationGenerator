using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentationGenerator.JSON
{
    public class NavBar
    {
        //               Title            Page Title -  Page Path
        public List<NavTitle> Titles { get; set; } = new();
    }

    public struct NavTitle
    {
        public string Title { get; set; }
        public Dictionary<string, string> Pages { get; set; }
    }
}
