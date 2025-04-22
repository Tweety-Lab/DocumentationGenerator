using DocumentationGenerator.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DocumentationGenerator.Utilities.Themes;

public static class ThemeUtil
{
    // Convert theme.json into a Theme object
    public static Theme LoadTheme(string themePath)
    {
        // Create a Theme Object
        Theme theme = new Theme();

        // Deserialize JSON
        string json = File.ReadAllText(themePath);
        theme = JsonSerializer.Deserialize<Theme>(json);

        return theme;
    }
}
