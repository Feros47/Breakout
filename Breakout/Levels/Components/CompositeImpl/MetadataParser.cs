namespace Breakout.Levels.Components.CompositeImpl;

using System;
using System.Collections.Generic;

/// <summary>
/// Parser class for the "Meta" section of the level configuration.
/// </summary>
public class MetadataParser : IComposite {
    public List<IComponent> Children {
        get;
    }
    public MetadataParser() {
        Children = new List<IComponent>();
    }

    /// <summary>
    /// Parses the metadata section string by splitting it on newlines and passing it to children responsible for parsing each line.
    /// </summary>
    /// <param name="metadataSection">Metadata string from the level file.</param>
    public void Parse(string metadataSection) {
        var attributes = metadataSection
            .Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        foreach (var attr in attributes) {
            var parser = new MetadataAttributeParser();
            parser.Parse(attr);
            Children.Add(parser);
        }
    }

    /// <summary>
    /// Populate a <see cref="LevelData"/> object with the current node and its subtree.
    /// </summary>
    /// <param name="data">The data object to populate.</param>
    public void Populate(LevelData data) {
        data.Metadata = new Metadata();
        foreach (var child in Children) {
            child.Populate(data);
        }
    }
}
