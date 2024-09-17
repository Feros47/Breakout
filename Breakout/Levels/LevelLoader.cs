namespace Breakout.Levels;

using System;
using Breakout.Exceptions;
using Breakout.Levels.Components.CompositeImpl;

/// <summary>
/// Static utility class providing a method for actually loading a level file.
/// </summary>
public static class LevelLoader {
    /// <summary>
    /// Loads a level file into a <see cref="LevelData"/> object. This is done in two phases:
    /// (1) Building a composite tree of the configuration text
    /// (2) Populating a <see cref="LevelData"/> instance using this tree structure.
    /// </summary>
    /// <param name="filePath">Path to the level file, to load</param>
    /// <returns>null if an error occured (invalid file, or not found), otherwise a <see cref="LevelData"/> object representing the level.</returns>
    public static LevelData? LoadFromConfiguration(string filePath) {
        var parser = new LevelDataParser();
        LevelData? level = new();
        try {
            using (var stream = new StreamReader(filePath)) {
                var contents = stream.ReadToEnd();
                parser.Parse(contents);
            }

            parser.Populate(level);
        } catch (ParsingException e) {
            Console.WriteLine($"An error occurred while loading level from file '{filePath}': {e.Message}");
            level = null;
        } catch (FileNotFoundException) {
            Console.WriteLine("Error: file-not-found");
            level = null;
        }

        return level;
    }
}
