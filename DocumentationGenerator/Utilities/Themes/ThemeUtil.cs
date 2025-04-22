using DocumentationGenerator.Themes;
using System;
using System.IO;
using System.Text.Json;

namespace DocumentationGenerator.Utilities.Themes;

public static class ThemeUtil
{
    /// <summary>
    /// Deserializes a theme.json file into a Theme object.
    /// Resolves relative paths against the executable's directory.
    /// </summary>
    public static Theme LoadTheme(string themePath)
    {
        try
        {
            string baseDir = AppContext.BaseDirectory;
            string fullPath = Path.IsPathRooted(themePath)
                ? themePath
                : Path.GetFullPath(Path.Combine(baseDir, themePath));

            Console.WriteLine($"Loading Theme from: {fullPath}");

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string json = File.ReadAllText(fullPath);
            Theme theme = JsonSerializer.Deserialize<Theme>(json, options);

            // Initialize Paths if null
            theme.Paths ??= new ThemePaths();
            theme.Paths.Root = Path.GetDirectoryName(fullPath);

            Console.WriteLine($"Loaded Theme Path - Root: {theme.Paths.Root}");
            Console.WriteLine($"Loaded Theme Path - Page: {theme.Paths.Page}");
            Console.WriteLine($"Loaded Theme Path - NavBar: {theme.Paths.NavBar}");

            return theme;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load Theme: {ex}");
            throw;
        }
    }
}
