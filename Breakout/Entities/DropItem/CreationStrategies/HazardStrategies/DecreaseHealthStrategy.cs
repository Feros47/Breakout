namespace Breakout.Entities.DropItem.CreationStrategies.HazardStrategies;

using Breakout.Utility;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
/// <summary>
/// Strategy for creation of player health decrease dropitem and logic for expending hazard
/// </summary>
public class DecreaseHealthStrategy : IDropItemCreationStrategy {

    public IBaseImage PowerupIcon => DIKUArcadeExtensions.LoadImageFromAssets("LoseLife.png");
    public DecreaseHealthStrategy() {
    }

    /// <summary>
    /// Create a new expendable item at the given position.
    /// </summary>
    /// <param name="pos"></param>
    public void CreateExpendable(Vec2F pos) {
        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
            EventType = GameEventType.StatusEvent,
            Message = "DECREASEHEALTH",
        });
    }

    /// <summary>
    /// Create a new drop item at the given position.
    /// </summary>
    /// <param name="pos"></param>
    /// <returns>
    /// A new drop item.
    /// </returns>
    public DropItem CreateItem(Vec2F pos) {
        var shape = new DynamicShape(pos, new Vec2F(0.05f, 0.05f));
        return new DropItem(shape, PowerupIcon, this);
    }

}
