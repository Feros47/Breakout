namespace Breakout.Levels.Components.LeafImpl;

using DIKUArcade.Math;

/// <summary>
/// Parser for a single map element. Since it needs to get the position from its parent, and the block type
/// is only a single character, its rather simple.
/// </summary>
public class MapElementParser : IComponent {
    public MapElement Element {
        get; set;
    }
    public MapElementParser(char type, int xGridPos, int yGridPos) {
        Element = new MapElement(new Vec2I(xGridPos, yGridPos), type);
    }

    /// <summary>
    /// Add the current element to the <see cref="LevelData"/> object's ASCII map list.
    /// </summary>
    /// <param name="level"><see cref="LevelData"/> object to populate.</param>
    public void Populate(LevelData level) {
        level.AsciiMap.Add(Element);
    }
}
