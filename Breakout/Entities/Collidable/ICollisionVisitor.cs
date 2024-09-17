namespace Breakout.Entities.Collidable;

using Breakout.Entities.Blocks;
using Breakout.Entities.Blocks.Decorators;
using Breakout.Entities.DropItem;
using Breakout.Entities.DropItem.PowerUps;
using DIKUArcade.Physics;

/// <summary>
/// Interface to determine how a given type should collide "on" another type.
/// i.e. <see cref="Visitors.PlayerCollisionVisitor.Collide(Ball, DIKUArcade.Physics.CollisionData)"/> defines
/// how a <see cref="Ball"/> is affected by a <see cref="Player"/> colliding on it. Conversely <see cref="Visitors.BallCollisionVisitor.Collide(Player, DIKUArcade.Physics.CollisionData)"/>
/// defines how a <see cref="Player"/> is affected by a <see cref="Ball"/>.
/// </summary>
public interface ICollisionVisitor {
    void Collide(Player player, CollisionData collisionData);
    void Collide(Ball ball, CollisionData collisionData);
    void Collide(BaseBlock component, CollisionData collisionData);
    void Collide(MovingBlockDecorator blockDecorator, CollisionData collisionData);
    void Collide(DropItem powerUpItem, CollisionData collisionData);
    void Collide(PowerUpExpendLaser powerUpLaser, CollisionData collisionData);
    void Collide(PowerUpExpendRocket powerUpTNT, CollisionData collisionData);
}