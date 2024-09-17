
namespace Breakout.Entities.Collidable.Visitors;

using Breakout.Entities.Blocks;
using Breakout.Entities.Blocks.Decorators;
using Breakout.Entities.DropItem;
using Breakout.Entities.DropItem.PowerUps;
using DIKUArcade.Physics;

/// <summary>
/// Visitor defining what damage a <see cref="Player"/> does to given objects, when it collides upon them.
/// </summary>
public class PlayerCollisionVisitor : ICollisionVisitor {
    private readonly Player player;
    public PlayerCollisionVisitor(Player player) {
        this.player = player;
    }

    /// <summary>
    /// Handle the collision of the ball, and change the direction of the ball depending on the player direction
    /// </summary>
    /// <param name="ball"></param>
    /// <param name="collisionData"></param>
    public void Collide(Ball ball, CollisionData collisionData) {
        ball.ChangeDirection(collisionData, player.GetDirection());
    }

    /// <summary>
    /// Handle the collision of the block, and expend the power up.
    /// </summary>
    /// <param name="powerUpItem"></param>
    /// <param name="collisionData"></param>
    public void Collide(DropItem powerUpItem, CollisionData collisionData) {
        powerUpItem.ExpendPowerUp();
        powerUpItem.DeleteEntity();
    }
    #region Empty implementation
    public void Collide(Player player, CollisionData collisionData) {
    }
    public void Collide(BaseBlock block, CollisionData collisionData) {
    }
    public void Collide(PowerUpExpendLaser powerUpLaser, CollisionData collisionData) {
    }
    public void Collide(PowerUpExpendRocket powerUpTNT, CollisionData collisionData) {
    }
    public void Collide(MovingBlockDecorator blockDecorator, CollisionData collisionData) {
    }
    #endregion
}