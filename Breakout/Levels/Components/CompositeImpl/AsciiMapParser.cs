namespace Breakout.Levels.Components.CompositeImpl;

using System;
using System.Collections.Generic;
using Breakout.Exceptions;
using Breakout.Levels.Components.LeafImpl;
using DIKUArcade.Math;

/// <summary>
/// Parser class for the ASCII-map in the level files.
/// </summary>
public class AsciiMapParser : IComposite {
    public List<IComponent> Children {
        get;
    }
    public Vec2I GridSize {
        get; set;
    }
    public AsciiMapParser() {
        Children = new List<IComponent>();
        GridSize = new Vec2I();
    }

    /// <summary>
    /// Parse an ASCII-map string and populate the blocks as children of the current node.
    /// </summary>
    /// <param name="asciiMap">The string containing the ASCII-map.</param>
    /// <exception cref="ParsingException">If the input is invalid.</exception>
    public void Parse(string asciiMap) {
        // We reverse the list of lines such that the indexing in the "grid matrix" is (0,0) in the bottom left corner
        // which is consistent with the DIKUArcade coordinate system.
        var lines = asciiMap
            .Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Reverse()
            .ToList();

        if (lines.Count <= 0) {
            throw new ParsingException("Empty map section in configuration file.");
        }

        GridSize = new Vec2I(lines.First().Length, lines.Count);

        if (lines.Any(line => line.Length != lines[0].Length)) {
            throw new ParsingException("Not all rows in the map section have the same length.");
        }

        for (var row = 0; row < GridSize.Y; row++) {
            for (var col = 0; col < GridSize.X; col++) {
                if (lines[row][col] != '-') {
                    Children.Add(new MapElementParser(lines[row][col], col, row));
                }
            }
        }
    }

    /// <summary>
    /// Populate the level data with the ASCII-map.
    /// </summary>
    public void Populate(LevelData data) {
        data.AsciiMap = new AsciiMapContainer(GridSize);
        foreach (var child in Children) {
            child.Populate(data);
        }
    }
}
