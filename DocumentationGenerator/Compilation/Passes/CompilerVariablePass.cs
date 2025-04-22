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
            { "{{DOCUMENTATION_TITLE}}", page.Title },

            // This should be last
            { "{{HTML_DOCUMENTATION}}", page.HTMLDocumentationContents }
        };

        // Replace compiler variables with their value
        page.HTMLContents = HTMLUtil.ReplaceKeywords(HTMLTemplates.PageTemplate, replacements);
    }
}