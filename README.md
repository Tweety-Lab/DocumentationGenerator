# 🔧 Documentation Generator 🔧
*A lightweight C# tool for transforming Markdown (.md) documentation into fast, customizable static websites.*

Documentation Generator allows you to quickly turn Markdown files into a fully customizable, static website that can be deployed with no extra work. You can use an out-of-the-box Theme or modify the modular HTML files to perfectly fit any layout, color scheme or style.

## 📝 Getting Started
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

## ⚒️ Building The Site
Once you have a valid `navbar.json`, navigate to its location in your directory and run `DocumentationGenerator "./"` in the console. This will generate a `Build` folder containing all the .HTML, .CSS, .JS files, alongside any images or resources required for the site.

## Third-Party Libraries

Documentation Generator uses the following third-party C# libraries:

- **Fleck (v1.2.0)**
  - Licensed under the MIT License  
  - [View License](https://github.com/statianzo/Fleck/blob/master/LICENSE)

- **Markdig (v0.41.0)**
  - Licensed under the BSD-2-Clause License  
  - [View License](https://github.com/xoofx/markdig/blob/master/license.txt)

- **System.Runtime.Serialization.Json (v4.3.0)**
  - Licensed under the Microsoft End User License Agreement (MS-EULA)  
  - [View License](http://go.microsoft.com/fwlink/?LinkId=329770)
