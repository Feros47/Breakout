namespace Breakout.Entities.DropItem.CreationStrategies.PowerUpStrategies;

using Breakout.Entities.DropItem.PowerUps;
using Breakout.Utility;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

/// <summary>
/// Strategy for creation of rocket dropitem and logic for expending Power Up
/// </summary>
public class RocketStrategy : IDropItemCreationStrategy {
    private PowerUpExpendRocket rocket;
    public IBaseImage PowerupIcon => DIKUArcadeExtensions.LoadImageFromAssets("RocketPickUp.png");

    public RocketStrategy() {
        rocket = new PowerUpExpendRocket();
    }

    /// <summary>
    /// Create a rocket.
    /// </summary>
    /// <param name="pos"></param>
    public void CreateExpendable(Vec2F pos) {
        rocket.Shape.SetPosition(pos);
        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
            EventType = GameEventType.GraphicsEvent,
            Message = "POWERUP_EXPEND",
            ObjectArg1 = rocket,
        });
    }

    /// <summary>
    /// Create a new drop item at the given position.
    /// </summary>
    /// <param name="pos"></param>
    /// <returns>DropItem with the powerup icon</returns>
    public DropItem CreateItem(Vec2F pos) {
        var shape = new DynamicShape(pos, new Vec2F(0.07f, 0.07f));
        return new DropItem(shape, PowerupIcon, this);
    }
}