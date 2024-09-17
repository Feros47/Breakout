namespace Breakout.Entities.Collidable.Visitors;

using Breakout.Entities.Blocks;
using Breakout.Entities.Blocks.Decorators;
using Breakout.Entities.DropItem;
using Breakout.Entities.DropItem.PowerUps;
using DIKUArcade.Physics;
/// <summary>
/// Visitor defining what damage a <see cref="Ball"/> does to given objects, when it collides upon them.
/// </summary>
public class BallCollisionVisitor : ICollisionVisitor {
    /// <summary>
    /// Change direction of other ball
    /// </summary>
    /// <param name="ball"></param>
    /// <param name="collisionData"></param>
    public void Collide(Ball ball, CollisionData collisionData) {
        ball.ChangeDirection(collisionData);
    }
    /// <summary>
    /// Let block handle collision
    /// </summary>
    /// <param name="component"></param>
    /// <param name="collisionData"></param>
    public void Collide(BaseBlock component, CollisionData collisionData) {
        component.HandleCollision();
    }
    #region Empty implementations
    public void Collide(Player player, CollisionData collisionData) {
    }
    public void Collide(DropItem powerUpItem, CollisionData collisionData) {
    }

    public void Collide(PowerUpExpendLaser powerUpLaser, CollisionData collisionData) {
    }

    public void Collide(PowerUpExpendRocket powerUpTNT, CollisionData collisionData) {
    }
    public void Collide(MovingBlockDecorator blockDecorator, CollisionData collisionData) {
    }


    #endregion
}