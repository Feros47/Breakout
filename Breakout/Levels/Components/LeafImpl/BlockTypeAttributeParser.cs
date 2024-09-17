namespace Breakout.Levels.Components.LeafImpl;

using Breakout.Entities.Blocks.Factories;
using Breakout.Exceptions;

/// <summary>
/// BlockTypeAttributeParser is responsible for parsing single-character metadata keys
/// which are associated with specific block type attributes (hardened, unbreakable etc.)
/// </summary>
public class BlockTypeAttributeParser : IComponent {
    private FactoryRegistry factoryRegistry;
    public char BlockType {
        get; set;
    }
    public IBlockFactory Factory {
        get; set;
    }
    /// <summary>
    /// Base a blocktypy and a related attribute into the character key and factory.
    /// </summary>
    /// <param name="blockType">String holding the block type. Note this could be a char, but then the above tree nodes would have to handle any invalid values.</param>
    /// <param name="attribute">The attribute of the block (Hardened, Moving etc..)</param>
    /// <exception cref="ArgumentOutOfRangeException">If either the blockType string isn't a single character or the attribute type doesn't exist in the factory registry.</exception>
    /// <exception cref="ParsingException">If blockType == '-' which is a reserved value.</exception>
    public BlockTypeAttributeParser(string blockType, string attribute) {
        factoryRegistry = new FactoryRegistry();
        if (blockType.Length != 1) {
            throw new ArgumentOutOfRangeException($"Expected '{nameof(blockType)}' string to have a single character. Got '{blockType}' (length: {blockType.Length})");
        } else if (blockType.First() == '-') {
            throw new ParsingException("Block type '-' is reserved to mean regular blocks and cannot be specified as a metadata attribute!");
        } else if (factoryRegistry[attribute] == null) {
            throw new ArgumentOutOfRangeException($"{nameof(blockType)} did not contain a valid block type. Got '{blockType}'");
        }
        BlockType = blockType.First();
        Factory = factoryRegistry[attribute]!;
    }
    /// <summary>
    /// Add the current key value pair to <see cref="Metadata.Factories"/>
    /// </summary>
    /// <param name="data">The data structure to populate.</param>
    public void Populate(LevelData data) {
        data.Metadata.Factories.Add(BlockType, Factory);
    }
}
