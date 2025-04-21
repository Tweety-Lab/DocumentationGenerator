# 🔧 Documentation Generator 🔧
*A C# Tool that converts `.md` documentation into a highly customizable static website.*

Documentation Generator allows you to quickly turn Markdown files into a fully customizable, static website that can be deployed with no extra work. You can use an out-of-the-box Theme or modify the modular HTML files to perfectly fit any layout, color scheme or style.

## Getting Started
To begin creating a Documentation Generator site, download the setup from the `Releases` section. After installing Documentation Generator, create a new folder to hold your Markdown files and add a `navbar.json` file. This file controls how pages in your documentation ara accessed through the Navigation Bar.

This is an example of a valid `navbar.json`
```JSON
{
  "Titles": [
    {
      "Title": "Fruits",
      "Pages": {
        "Tomato": "tomato.md",
        "Apple": "apple.md",
      }
    },
    {
      "Title": "Vegetables",
      "Pages": {
        "Carrot": "carrot.md",
        "Lettuce": "lettuce.md",
      }
    }
  ]
}
```
This makes 2 titles, 'Fruits' and 'Vegetables', each with two pages linking to a .md file.

### Building The Site
Once you have a valid `navbar.json`, navigate to its location in your directory and run `DocumentationGenerator "./"` in the console. This will generate a `Build` folder containing all the .HTML, .CSS, .JS files, alongside any images or resources required for the site.
