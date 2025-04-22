using DocumentationGenerator.Themes;
using DocumentationGenerator.Utilities.Themes;

namespace DocumentationGenerator.Utilities.HTML;

public static class HTMLTemplates
{
    public static Theme Theme { get; set; } = ThemeUtil.LoadTheme(Path.Combine(AppContext.BaseDirectory, "../../../HTMLTemplates/Minimal/theme.json"));
    public static string PageTemplate { get; set; } = File.ReadAllText(Path.Combine(Theme.Paths.Root, Theme.Paths.Page));
    public static string NavBarTemplate { get; set; } = File.ReadAllText(Path.Combine(Theme.Paths.Root, Theme.Paths.NavBar));
}