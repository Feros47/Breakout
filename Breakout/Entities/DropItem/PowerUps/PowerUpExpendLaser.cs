
namespace Breakout.Entities.DropItem.PowerUps;

using Breakout.Entities.Collidable;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;

/// <summary>
/// PowerUpExpendLaser is a powerup that makes the paddle expend lasers.
/// </summary>
public class PowerUpExpendLaser : PowerUpExpend {

    /// <summary>
    /// Constructor for PowerUpExpendLaser.
    /// </summary>
    /// <returns>PowerUpExpendLaser</returns>
    public PowerUpExpendLaser() : base(
        new DynamicShape(new Vec2F(0f, 0f), new Vec2F(0.015f, 0.15f), new Vec2F(0f, 0.04f)),
        new Image(Path.Combine("..", "Breakout", "Assets", "Images", "BulletRed2.png"))
        ) {
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
