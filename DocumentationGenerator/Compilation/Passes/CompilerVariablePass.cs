using DocumentationGenerator.Utilities.HTML;
using DocumentationGenerator.Serializing;

namespace DocumentationGenerator.Compilation.Passes;

/// <summary>
///     Add Compiler Variables to the page.
/// </summary>
public class CompilerVariablePass : ICompilerPass
{
    public void Execute(Page page)
    {
        // Define compiler variable and their values
        var replacements = new Dictionary<string, string>
        {
            { "{{NAVBAR}}", page.HTMLNavBarContents },
            { "{{HTML_DOCUMENTATION}}", page.HTMLDocumentationContents },
            { "{{DOCUMENTATION_TITLE}}", page.Title }
        };

        // Replace compiler variables with their value
        page.HTMLContents = HTMLUtil.ReplaceKeywords(HTMLTemplates.PageTemplate, replacements);
    }
}