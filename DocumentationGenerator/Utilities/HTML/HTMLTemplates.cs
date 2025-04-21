namespace DocumentationGenerator.FileUtilities.HTML;

public static class HTMLTemplates
{
    public static string TemplatePath { get; set; } = Path.Combine(AppContext.BaseDirectory, "../../../HTMLTemplates");
    public static string PageTemplate { get; set; } = File.ReadAllText(Path.Combine(TemplatePath, "test_page.html"));

    public static string NavBarTemplate { get; set; } =
        File.ReadAllText(Path.Combine(TemplatePath, "test_navbar.html"));
}