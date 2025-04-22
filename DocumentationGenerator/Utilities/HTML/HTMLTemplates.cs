using DocumentationGenerator.Themes;
using DocumentationGenerator.Utilities.Themes;

namespace DocumentationGenerator.Utilities.HTML;

public static class HTMLTemplates
{
    public static Theme Theme { get; set; } = ThemeUtil.LoadTheme("../../../HTMLTemplates/Minimal/theme.json");
    public static string PageTemplate => File.ReadAllText(Path.Combine(Theme.Paths.Root, Theme.Paths.Page));
    public static string NavBarTemplate => File.ReadAllText(Path.Combine(Theme.Paths.Root, Theme.Paths.NavBar));
}