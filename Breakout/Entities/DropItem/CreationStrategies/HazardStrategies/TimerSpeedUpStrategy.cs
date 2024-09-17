namespace Breakout.Entities.DropItem.CreationStrategies.HazardStrategies;
using Breakout.Utility;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;

/// <summary>
/// Strategy for creation of timer speed up dropitem and logic for expending hazard
/// </summary>
public class TimerSpeedUpStrategy : IDropItemCreationStrategy {

    public IBaseImage PowerupIcon => DIKUArcadeExtensions.LoadImageFromAssets("clock-down.png");
    public TimerSpeedUpStrategy() {
    }

    /// <summary>
    /// Decrease the clock speed and increase it after 5 seconds
    /// </summary>
    /// <param name="pos">Position of the dropitem</param>
    public void CreateExpendable(Vec2F pos) {
        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
            EventType = GameEventType.StatusEvent,
            Message = "CLOCK_DOWN",
        });
        BreakoutBus.GetBus().AddOrResetTimedEvent(new GameEvent {
            EventType = GameEventType.StatusEvent,
            Message = "CLOCK_NORMAL",
            Id = (int) TimedEventType.TimerEvent,
        }, TimePeriod.NewSeconds(5));
    }

    /// <summary>
    /// Create a dropitem with the powerup icon
    /// </summary>
    /// <param name="pos">Position of the dropitem</param>
    /// <returns>DropItem with the powerup icon</returns>
    public DropItem CreateItem(Vec2F pos) {
        var shape = new DynamicShape(pos, new Vec2F(0.05f, 0.05f));
        return new DropItem(shape, PowerupIcon, this);
    }
}