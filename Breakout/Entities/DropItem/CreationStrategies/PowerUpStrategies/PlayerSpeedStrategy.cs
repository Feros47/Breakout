namespace Breakout.Entities.DropItem.CreationStrategies.PowerUpStrategies;

using Breakout.Utility;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;

/// <summary>
/// Strategy for creation of Player speed up dropitem and logic for expending Power Up
/// </summary>
public class PlayerSpeedStrategy : IDropItemCreationStrategy {

    public IBaseImage PowerupIcon => DIKUArcadeExtensions.LoadImageFromAssets("DoubleSpeedPowerUp.png");
    public PlayerSpeedStrategy() {
    }

    /// <summary>
    /// Increase the player's speed, and decrease it after 5 seconds.
    /// </summary>
    /// <param name="pos"></param>
    public void CreateExpendable(Vec2F pos) {
        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
            EventType = GameEventType.PlayerEvent,
            Message = "POWERUP_EXPEND_PLAYER_INCREASESPEED",
            Id = (int) TimedEventType.PlayerSpeedPowerUp,
        });

        BreakoutBus.GetBus().AddOrResetTimedEvent(new GameEvent {
            EventType = GameEventType.PlayerEvent,
            Message = "POWERUP_EXPEND_PLAYER_DECREASESPEED",
            Id = (int) TimedEventType.PlayerSpeedPowerUp,
        }, TimePeriod.NewSeconds(5));
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