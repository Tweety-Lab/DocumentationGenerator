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
    /// <summary>
    /// Deserializes a theme.json file into a Theme object.
    /// </summary>
    public static Theme LoadTheme(string themePath)
    {
        try
        {
            Console.WriteLine($"Loading With Theme: {themePath}");

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string json = File.ReadAllText(themePath);
            Theme theme = JsonSerializer.Deserialize<Theme>(json, options);

            // Initialize Paths if null
            theme.Paths ??= new ThemePaths();
            theme.Paths.Root = Path.GetDirectoryName(themePath);

            Console.WriteLine($"Loaded Theme Path - Root: {theme.Paths.Root}");
            Console.WriteLine($"Loaded Theme Path - Page: {theme.Paths.Page}");
            Console.WriteLine($"Loaded Theme Path - NavBar: {theme.Paths.NavBar}");

            return theme;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load Theme: {ex}");
            return null;
        }
    }
}
