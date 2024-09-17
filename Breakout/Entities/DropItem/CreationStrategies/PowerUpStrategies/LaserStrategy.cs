namespace Breakout.Entities.DropItem.CreationStrategies.PowerUpStrategies;

using Breakout.Entities.DropItem.PowerUps;
using Breakout.Utility;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

/// <summary>
/// Strategy for creation of Laser dropitem and logic for expending Power Up
/// </summary>
public class LaserStrategy : IDropItemCreationStrategy {
    private PowerUpExpendLaser laser;
    public IBaseImage PowerupIcon => DIKUArcadeExtensions.LoadImageFromAssets("DamagePickUp.png");

    public LaserStrategy() {
        laser = new PowerUpExpendLaser();
    }

    /// <summary>
    /// Create a laser.
    /// </summary>
    /// <param name="pos"></param>
    public void CreateExpendable(Vec2F pos) {
        laser.Shape.SetPosition(pos);
        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
            EventType = GameEventType.GraphicsEvent,
            Message = "POWERUP_EXPEND",
            ObjectArg1 = laser,
        });
    }

    /// <summary>
    /// Create a new drop item at the given position.
    /// </summary>
    /// <param name="pos"></param>
    /// <returns>DropItem with the powerup icon</returns>
    public DropItem CreateItem(Vec2F pos) {
        var shape = new DynamicShape(pos, new Vec2F(0.02f, 0.08f));
        return new DropItem(shape, PowerupIcon, this);
    }
}