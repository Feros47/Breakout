namespace Breakout.Levels;

using Breakout.Entities.Blocks.Factories;

/// <summary>
/// Mapping from characters to block factories.
/// </summary>
public class BlockFactoryMap {
    private readonly Dictionary<char, List<IBlockFactory>> factories;
    public BlockFactoryMap() {
        factories = new();
    }
    public IReadOnlyList<IBlockFactory> this[char key] {
        get {
            if (!factories.ContainsKey(key)) {
                factories[key] = new List<IBlockFactory>();
            }
            return factories[key];
        }
    }

    /// <summary>
    /// Register a factory for a given block type.
    /// </summary>
    /// <param name="key">The block type</param>
    /// <param name="factory">Factory to register</param>
    public void Add(char key, IBlockFactory factory) {
        if (!factories.ContainsKey(key)) {
            factories[key] = new List<IBlockFactory>();
        }
        // We prepend the element since order is reversed in the sense that when a block is declared as hardened and then powerup in the configuration
        // we want to firstly add a power up decorator and then a hardened decorator, since the hardened should be the outermost "layer".
        factories[key] = factories[key]
            .Prepend(factory)
            .ToList();
    }
}
