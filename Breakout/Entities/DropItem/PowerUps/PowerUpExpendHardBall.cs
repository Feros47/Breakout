
namespace Breakout.Entities.DropItem.PowerUps;

using Breakout.Entities.Collidable;
using Breakout.Entities.Collidable.Visitors;
using DIKUArcade.Graphics;

/// <summary>
/// PowerUpExpendHardBall is a powerup that makes the ball hard.
/// </summary>
public class PowerUpExpendHardBall : Ball {

    /// <summary>
    /// Constructor for PowerUpExpendHardBall.
    /// </summary>
    /// <returns>PowerUpExpendHardBall</returns>
    public PowerUpExpendHardBall() : base(
        new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball2.png"))
        ) {
    }

    public override ICollisionVisitor MakeVisitor() {
        return new HardBallCollisionVisitor();
    }
}
