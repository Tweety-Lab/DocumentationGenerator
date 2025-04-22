# Compiler Variables
Compiler Variables allow you to create flexible theme templates by inserting dynamic values from the site and markdown data without hard-coding them. During compilation, each variable gets replaced with its actual value. Variables use the format `{{VARIABLE_NAME}}`.

| Variable  | Purpose |
|-----------|-----|
| {{HTML_DOCUMENTATION}}  | The compiled HTML for the page's markdown  |
| {{DOCUMENTATION_TITLE}} | The name of the page  |
| {{NAVBAR}} | The HTML of the Theme's navbar.  |
| {{NAVBAR_CONTENT}} | A Unordered List that includes all the navbar Titles and pages.  |

## Examples
```HTML
<body>
    <div class="navbar">
        {{NAVBAR_CONTENT}}
    </div>
</body>
```