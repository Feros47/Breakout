namespace Breakout.Levels;
using DIKUArcade.Math;

/// <summary>
/// A single element in the ASCII map of a level file.
/// </summary>
public class MapElement {
    public MapElement(Vec2I position, char type) {
        GridPosition = position;
        BlockType = type;
    }
    /// <summary>
    /// (x,y) position in the "grid" created by the ASCII-map.
    /// </summary>
    public Vec2I GridPosition { get; set; } = new();

    /// <summary>
    /// The block type/character.
    /// </summary>
    public char BlockType {
        get; set;
    }
}
