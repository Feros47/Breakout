namespace Breakout.Entities.Collidable.Visitors;
using Breakout.Entities;
using Breakout.Entities.Blocks;
using Breakout.Entities.Blocks.Decorators;
using Breakout.Entities.Collidable;
using Breakout.Entities.DropItem;
using Breakout.Entities.DropItem.PowerUps;
using DIKUArcade.Physics;

/// <summary>
/// Visitor defining what damage a <see cref="UnbreakableBlockDecorator"/> does to given objects, when it collides upon them.
/// </summary>
public class UnbreakableBlockCollisionVisitor : ICollisionVisitor {
    public void Collide(Player player, CollisionData collisionData) {
    }
    /// <summary>
    /// Change the direction of the ball.
    /// </summary>
    /// <param name="ball"></param>
    /// <param name="collisionData"></param>
    public void Collide(Ball ball, CollisionData collisionData) {
        ball.ChangeDirection(collisionData);
    }
    public void Collide(DropItem powerUpItem, CollisionData collisionData) {
    }
    /// <summary>
    /// Delete the power up when it collides with the block.
    /// </summary>
    /// <param name="powerUpLaser"></param>
    /// <param name="collisionData"></param>
    public void Collide(PowerUpExpendLaser powerUpLaser, CollisionData collisionData) {
        powerUpLaser.DeleteEntity();
    }
    /// <summary>
    /// Delete the power up when it collides with the block.
    /// </summary>
    /// <param name="powerUpTNT"></param>
    /// <param name="collisionData"></param>
    public void Collide(PowerUpExpendRocket powerUpTNT, CollisionData collisionData) {
        powerUpTNT.DeleteEntity();
    }
    public void Collide(BaseBlock component, CollisionData collisionData) {
    }

    public void Collide(MovingBlockDecorator blockDecorator, CollisionData collisionData) {
    }
}