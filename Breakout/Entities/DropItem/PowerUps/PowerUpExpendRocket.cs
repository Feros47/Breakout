namespace Breakout.Entities.DropItem.PowerUps;

using Breakout.Entities.Collidable;
using Breakout.Utility;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Physics;

/// <summary>
/// PowerUpExpendRocket is a powerup that makes the paddle expend rockets.
/// </summary>
public class PowerUpExpendRocket : PowerUpExpend {

    /// <summary>
    /// Constructor for PowerUpExpendRocket.
    /// </summary>
    /// <returns>PowerUpExpendRocket</returns>
    public PowerUpExpendRocket() : base(
        new DynamicShape(new Vec2F(0f, 0f), new Vec2F(0.1f, 0.08f), new Vec2F(0f, 0.03f)),
        DIKUArcadeExtensions.LoadStridesFromAssets("rocketLaunched.png", 5)) {
    }

    /// <summary>
    /// Accept a collision with a visitor.
    /// </summary>
    /// <param name="collisionVisitor">Visitor to accept collision with.</param>
    /// <param name="collisionData">Data about the collision.</param>
    public override void AcceptCollision(ICollisionVisitor collisionVisitor, CollisionData collisionData) {
        collisionVisitor.Collide(this, collisionData);
    }

}