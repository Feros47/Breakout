namespace Breakout.Levels.Components.CompositeImpl;

using System;
using System.Collections.Generic;
using Breakout.Exceptions;

/// <summary>
/// Root node of the composite tree of a parsed level file.
/// </summary>
public class LevelDataParser : IComposite {
    public List<IComponent> Children {
        get;
    }
    public LevelDataParser() {
        Children = new List<IComponent>();
    }

    /// <summary>
    /// Parse an entire level file.
    /// </summary>
    /// <param name="configurationFileContent">The string to parse, read from a file.</param>
    /// <exception cref="ParsingException">In case of an invalid input.</exception>
    public void Parse(string configurationFileContent) {
        var lines = configurationFileContent
            .Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .ToList();
        if (lines.Count == 0) {
            throw new ParsingException($"empty string was passed to {nameof(LevelDataParser)}");
        }

        var enumerator = lines.GetEnumerator();
        while (enumerator.MoveNext()) {
            var line = enumerator.Current;

            // Beginning of section
            if (line.EndsWith(':')) {
                // When ReadSection returns, we have either passed the entire list, or reached the "end"-tag
                // so the enumerator has passed the entire section.
                var name = line.Substring(0, line.Length - 1);
                enumerator = ReadSection(enumerator, name, out var sectionData);

                IComposite? parser = null;
                parser = name.ToLower() switch {
                    "map" => new AsciiMapParser(),
                    "meta" => new MetadataParser(),
                    "legend" => new LegendParser(),
                    _ => throw new ParsingException("unexpected section name: " + name),
                };
                parser.Parse(sectionData);
                Children.Add(parser);
            }
        }
    }

    /// <summary>
    /// Populate the data in a <see cref="LevelData"/> instance using our parsed children nodes.
    /// </summary>
    /// <param name="data">The object to populate.</param>
    public void Populate(LevelData data) {
        foreach (var child in Children) {
            child.Populate(data);
        }
    }

    /// <summary>
    /// Read a list of strings/lines (newline-separated text) until the end of a given section.
    /// </summary>
    /// <param name="iterator">An iterator into the list.</param>
    /// <param name="sectionName">The name of the section to "seek".</param>
    /// <param name="sectionData">(output) all data passed until either EOF, or the section end-tag was found.</param>
    /// <returns>The input iterator (in order to signify that it has been changed)</returns>
    public static List<string>.Enumerator ReadSection(List<string>.Enumerator iterator, string sectionName, out string sectionData) {
        sectionData = "";

        while (iterator.MoveNext() && iterator.Current != $"{sectionName}/") {
            sectionData += $"{iterator.Current}\n";
        }
        // We return the iterator again to emphasize that it is changed by the method.
        return iterator;
    }
}
