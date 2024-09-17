namespace Breakout.Levels.Components.CompositeImpl;

using System;
using System.Collections.Generic;
using Breakout.Levels.Components.LeafImpl;

/// <summary>
/// Class responsible for parsing the legend section of the level file, passing individual entries to child-nodes in the composite tree.
/// </summary>
public class LegendParser : IComposite {
    public List<IComponent> Children {
        get;
    }
    public LegendParser() {
        Children = new List<IComponent>();
    }

    /// <summary>
    /// Split the legend section on newlines and let <see cref="LegendAttributeParser"/> objects parse individual entries,
    /// and the results as children.
    /// </summary>
    /// <param name="legendSection">The legend-string from the level file.</param>
    public void Parse(string legendSection) {
        var legends = legendSection
            .Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        foreach (var legend in legends) {
            Children.Add(new LegendAttributeParser(legend));
        }
    }
    /// <summary>
    /// Populate the <see cref="LegendData"/> dictionary inside of a <see cref="LevelData"/> object.
    /// </summary>
    /// <param name="data">The object to populate.</param>
    public void Populate(LevelData data) {
        data.LegendData = new LegendData();
        foreach (var child in Children) {
            child.Populate(data);
        }
    }
}
