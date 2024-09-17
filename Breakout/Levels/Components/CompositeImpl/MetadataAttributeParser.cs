namespace Breakout.Levels.Components.CompositeImpl;

using System;
using Breakout.Entities.Blocks.Factories;
using Breakout.Exceptions;
using Breakout.Levels.Components.LeafImpl;
using Breakout.Utility;

/// <summary>
/// Class responsible for parsing a single entry in the meta-section.
/// Example: "NAME: Level 1"
/// </summary>
public class MetadataAttributeParser : IComposite {
    private readonly FactoryRegistry factoryRegistry;
    public MetadataAttributeParser() {
        factoryRegistry = new();
        Children = new List<IComponent>();
        Key = string.Empty;
        Value = string.Empty;
    }
    public List<IComponent> Children {
        get; private set;
    }
    public string Key {
        get; set;
    }
    public string Value {
        get; set;
    }

    /// <summary>
    /// Parse an entry in the metadata section.
    /// </summary>
    /// <param name="metadataLine">The line to parse.</param>
    /// <exception cref="ParsingException">If the entry is invalid</exception>
    public void Parse(string metadataLine) {
        try {
            var keyValuePair = metadataLine.SplitOnFirstAndTrim(':');
            Key = keyValuePair.Item1;
            Value = keyValuePair.Item2;

            // If this metadata attribute refers to block types, add block type parsers as children
            if (factoryRegistry.IsBlockTypeDesignator(Key)) {
                var blockTypes = Value.SplitAndTrim(',');
                Children.AddRange(
                    blockTypes
                        .Select(bt => new BlockTypeAttributeParser(bt, Key)));
            }
        } catch (ArgumentOutOfRangeException e) {
            throw new ParsingException("an error occurred while parsing metadata attribute", e);
        }
    }
    /// <summary>
    /// Populate the <see cref="Metadata"/> instance inside of a <see cref="LevelData"/> object.
    /// </summary>
    /// <param name="data">Object to populate</param>
    /// <exception cref="ParsingException">If data.Metadata already contains Key.</exception>
    public virtual void Populate(LevelData data) {
        try {
            data.Metadata[Key] = Value;

            foreach (var child in Children) {
                child.Populate(data);
            }
        } catch (InvalidOperationException e) {
            throw new ParsingException($"an error occurred while populating {nameof(data.Metadata)}", e);
        }
    }
}
