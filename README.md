# TextParser

## Summary
This is a small project I took upon to explore and find out a little more about text parsing. The general goal is completed but the code can certainly be improved and optimized. On top of adding better practices

## Goals:
 
- Parsing Text into different characters and keywords ✅
- Improving Test Coverage 
- Adding Validation
- Adding Formatting capabilities to prettify the template
- Adding new ways of defining dictionaries either through multiple files or single file conversion.
- Adding a way to edit the symbols to be used for parsing and disabling feature. 
- Setup file watch on the Terminal level to ensure recompilation
- Create a GUI version with .Net MAUI
- Create a Web Implementation

## Ideas

- Using a `.conf` file that allows editing Symbols and code rules
- Introducing multiple ways to declare dictionaries and a one file for all method as well

## Usage

To use, one simply needs an asset folder where the font and keyword dictionaries are saved, if they don't exist no parsing would be done.

Any text inside `{{ }}` will be parsed and any outside of it will simply be copy pasted

using `!` will stop keyword conversion and using `|` will stop font conversion

NOTE : Font Conversion is simply converting into a different set of unicode characters, it is separated into it's own category in order to be able to allow or cancel it. This is to be improved later on

