namespace Breakout.Levels.Components.LeafImpl;

using System;
using Breakout.Exceptions;
using Breakout.Utility;

/// <summary>
/// Parser for the legend attributes of the form "c) Char.png"
/// </summary>
public class LegendAttributeParser : IComponent {
    public char Key {
        get; set;
    }
    public string Value {
        get; set;
    }
    public LegendAttributeParser(string legendLine) {
        try {
            var keyValuePair = legendLine.SplitOnFirstAndTrim(')');
            if (keyValuePair.Item1.Length != 1 || keyValuePair.Item1[0] >= 128) {
                throw new ParsingException($"Key '{keyValuePair.Item1}' is not an ASCII-character.");
            }
            Key = keyValuePair.Item1[0];
            Value = keyValuePair.Item2;
        } catch (ArgumentOutOfRangeException e) {
            throw new ParsingException($"out-of-range while parsing legend: {e.Message}", e);
        }
    }
    /// <summary>
    /// Add the current key-value pair to the <see cref="LevelData"/> map.
    /// </summary>
    /// <param name="data">The data to populate</param>
    /// <exception cref="ParsingException">In case either the file specified by <see cref="Value"/> doesn't exist, or if the entry already exists in the dictionary.</exception>
    public void Populate(LevelData data) {
        try {
            data.LegendData.AddLegend(Key, Value);
        } catch (InvalidOperationException e) {
            throw new ParsingException($"an error occurred while populating {nameof(data.LegendData)}: {e.Message}", e);
        } catch (FileNotFoundException e) {
            throw new ParsingException($"an error occurred while populating {nameof(data.LegendData)}: {e.Message}", e);
        }
    }
}
