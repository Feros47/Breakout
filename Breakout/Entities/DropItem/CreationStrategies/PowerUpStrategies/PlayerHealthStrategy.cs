namespace Breakout.Entities.DropItem.CreationStrategies.PowerUpStrategies;
using Breakout.Utility;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

/// <summary>
/// Strategy for creation of Player health up dropitem and logic for expending Power Up
/// </summary>
public class PlayerHealthStrategy : IDropItemCreationStrategy {

    public IBaseImage PowerupIcon => DIKUArcadeExtensions.LoadImageFromAssets("heart_filled.png");
    public PlayerHealthStrategy() {
    }

    /// <summary>
    /// Increase the player's health.
    /// </summary>
    /// <param name="pos"></param>
    public void CreateExpendable(Vec2F pos) {
        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
            EventType = GameEventType.StatusEvent,
            Message = "INCREASEHEALTH",
        });
    }

    /// <summary>
    /// Create a new drop item at the given position.
    /// </summary>
    /// <param name="pos"></param>
    /// <returns>DropItem with the powerup icon</returns>
    public DropItem CreateItem(Vec2F pos) {
        var shape = new DynamicShape(pos, new Vec2F(0.05f, 0.05f));
        return new DropItem(shape, PowerupIcon, this);
    }
}