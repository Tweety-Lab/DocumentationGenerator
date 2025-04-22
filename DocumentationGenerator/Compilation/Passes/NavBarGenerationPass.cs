using DocumentationGenerator.Utilities.HTML;
using DocumentationGenerator.Serializing;

namespace DocumentationGenerator.Compilation.Passes;

/// <summary>
///     Compiles the Navigation Bar.
/// </summary>
public class NavBarGenerationPass : ICompilerPass
{
    public void Execute(Page page)
    {
        var navbarHTML = NavBarUtil.ConvertNavBarToHTML(Program.Builder.NavBar);
        var compiledNavBar = HTMLUtil.ReplaceKeyword(
            HTMLTemplates.NavBarTemplate, "{{NAVBAR_CONTENT}}", navbarHTML);

        page.HTMLNavBarContents = compiledNavBar;
    }
}