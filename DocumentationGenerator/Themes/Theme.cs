using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentationGenerator.Themes;

/// <summary>
/// Represents a theme.
/// </summary>
public class Theme
{
    // Metadata
    public string Name { get; set; }
    public string Description { get; set; }

    public ThemePaths Paths { get; set; }
}

public class ThemePaths
{
    public string Page { get; set; }
    public string NavBar { get; set; }
}
