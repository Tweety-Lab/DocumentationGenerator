using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentationGenerator.Markdown
{
    public static class HTMLTemplates
    {
        public static string PageTemplate = File.ReadAllText("HTMLTemplates/test_page.html");
        public static string NavBarTemplate = File.ReadAllText("HTMLTemplates/test_navbar.html");
    }
}
