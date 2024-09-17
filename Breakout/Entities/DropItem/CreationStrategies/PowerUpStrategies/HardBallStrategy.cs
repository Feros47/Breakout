namespace Breakout.Entities.DropItem.CreationStrategies.PowerUpStrategies;

using Breakout.Entities.DropItem.PowerUps;
using Breakout.Utility;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

/// <summary>
/// Strategy for creation of Hard ball dropitem and logic for expending Power Up
/// </summary>
public class HardBallStrategy : IDropItemCreationStrategy {
    private PowerUpExpendHardBall hardBall;
    public IBaseImage PowerupIcon => DIKUArcadeExtensions.LoadImageFromAssets("ball2.png");
    public HardBallStrategy() {
        ;
        hardBall = new PowerUpExpendHardBall();
    }
    /// <summary>
    /// Create a hardball.
    /// </summary>
    /// <param name="pos"></param>
    public void CreateExpendable(Vec2F pos) {
        hardBall.Shape.SetPosition(pos);
        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
            EventType = GameEventType.GraphicsEvent,
            Message = "POWERUP_EXPEND",
            ObjectArg1 = hardBall,
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