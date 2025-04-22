# Compiler Variables
Compiler Variables allow you to write generic Theme templates by accessing site and markdown data without hard-coding it. The compiler replaces all instances of a variable with its proper value when building a site.

| Variable  | Purpose |
|-----------|-----|
| {{HTML_DOCUMENTATION}}  | The compiled HTML for the page's markdown  |
| {{DOCUMENTATION_TITLE}} | The name of the page  |
| {{NAVBAR}} | The HTML of the Theme's navbar.  |
| {{NAVBAR_CONTENT}} | A Unordered List that includes all the navbar Titles and pages.  |
