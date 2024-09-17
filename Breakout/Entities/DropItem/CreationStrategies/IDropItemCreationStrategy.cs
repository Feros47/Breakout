namespace Breakout.Entities.DropItem.CreationStrategies;

using DIKUArcade.Graphics;
using DIKUArcade.Math;

/// <summary>
/// Interface for creation of drop items.
/// </summary>
public interface IDropItemCreationStrategy {
    public DropItem CreateItem(Vec2F pos);
    public void CreateExpendable(Vec2F pos);
    IBaseImage PowerupIcon {
        get;
    }
}