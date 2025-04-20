# Testing
In the Project root there's a `build.bat` file, this file builds the project and then uses it to generate documentation for the **Test Documentation located in "TestDocs"**. This outputs the built documentation to "Build".

# Compiling
Install VS2022 With C# Support

Open DocumentationGenerator.sln

Right Click "DocumentationGenerator" -> Build

# Reading the JSON Template
There's an example JSON file in the Project root named "JSON_TEMPLATE.json". The JSON defines a Navbar that stores data in a tree like format:

```
📂 JSON Root
├── 📝 Titles
│   ├── 📄 Title: Title Name
│   └── 📄 Page
│   │   └── 🆓 Button: Button Name - Markdown Path
```

An Example of valid navbar JSON is this:
```JSON
{
  "Titles": [
    {
      "Title": "This is a title",
      "Pages": {
        "THIS IS A BUTTON": "this is the path to the markdown file linked with the button"
      }
    },
    {
      "Title": "Fruits",
      "Pages": {
        "Apple": "pages/apple.md",
        "Banana": "pages/banana.md",
        "Orange": "pages/orange.md"
      }
    }
  ]
}
```
This is a navbar containing 2 titles: *"This is a title"* and *"Fruits"*. The *"Fruits"* Title includes 3 Buttons, the first one is labelled *"Apple"* which links to a markdown file located at *"pages/apple.md"*.
