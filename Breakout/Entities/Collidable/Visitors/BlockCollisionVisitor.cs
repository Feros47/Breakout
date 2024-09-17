#nullable disable
namespace Breakout.Entities.Collidable.Visitors;

using Breakout.Entities.Blocks;
using Breakout.Entities.Blocks.Decorators;
using Breakout.Entities.DropItem;
using Breakout.Entities.DropItem.PowerUps;
using Breakout.Utility;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;

/// <summary>
/// Visitor defining what damage a <see cref="BaseBlock"/> does to given objects, when it collides upon them.
/// </summary>
public class BlockCollisionVisitor : ICollisionVisitor {

    public void Collide(Ball ball, CollisionData collisionData) {
        ball.ChangeDirection(collisionData);
    }

    /// <summary>
    /// Handle the collision of the block, and create a rocket.
    /// </summary>
    /// <param name="powerUpExpendRocket"></param>
    /// <param name="collisionData"></param>
    public void Collide(PowerUpExpendRocket powerUpExpendRocket, CollisionData collisionData) {
        ImageStride stride = DIKUArcadeExtensions.LoadStridesFromAssets("Explosion.png", 8, 10) as ImageStride;
        StationaryShape shape = new StationaryShape(powerUpExpendRocket.GetShape().Position, new Vec2F(0.18f, 0.18f));
        var animation = new AnimationAdapter(200, shape, stride!);
        BreakoutBus.GetBus().RegisterEvent(new GameEvent() {
            EventType = GameEventType.GraphicsEvent,
            Message = "ROCKET_EXPLOSION",
            ObjectArg1 = animation,
        });
        powerUpExpendRocket.DeleteEntity();
    }
    public void Collide(PowerUpExpendLaser powerUpLaser, CollisionData collisionData) {
    }

    /// <summary>
    /// Handle the collision of the block, and change the moving block's direction.
    /// </summary>
    /// <param name="blockDecorator"></param>
    /// <param name="collisionData"></param>
    public void Collide(MovingBlockDecorator blockDecorator, CollisionData collisionData) {
        blockDecorator.ChangeDirection(collisionData.CollisionDir);
    }

    #region Empty implementations
    public void Collide(Player player, CollisionData collisionData) {
    }
    public void Collide(BaseBlock component, CollisionData collisionData) {
    }
    public void Collide(DropItem powerUpItem, CollisionData collisionData) {
    }

    #endregion
}