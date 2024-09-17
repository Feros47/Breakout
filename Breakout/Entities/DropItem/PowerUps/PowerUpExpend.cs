namespace Breakout.Entities.DropItem.PowerUps;

using Breakout.Entities.Collidable;
using Breakout.Entities.Collidable.Visitors;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;

/// <summary>
/// Abstract class for expendable powerups.
/// </summary>
public abstract class PowerUpExpend : BreakoutEntityBase, ICollidable {

    /// <summary>
    /// Constructor for PowerUpExpend.
    /// </summary>
    /// <param name="shape">Shape of the powerup.</param>
    public PowerUpExpend(Shape shape, IBaseImage img) : base(shape, img) { }

    public abstract void AcceptCollision(ICollisionVisitor collisionVisitor, CollisionData collisionData);

    /// <summary>
    /// Make a visitor for the powerup.
    /// </summary>
    /// <returns>PowerUpExpendCollisionVisitor</returns>
    public virtual ICollisionVisitor MakeVisitor() {
        return new PowerUpExpendCollisionVisitor();
    }

    /// <summary>
    /// Get the shape of the powerup.
    /// </summary>
    /// <returns>DynamicShape</returns>
    public DynamicShape GetShape() {
        return Shape.AsDynamicShape();
    }

    /// <summary>
    /// Check if the powerup should be ignored.
    /// </summary>
    /// <returns>bool</returns>
    public bool ShouldIgnore() {
        if (IsDeleted())
            return true;

        return false;
    }

    /// <summary>
    /// Update the powerup.
    /// </summary>
    public override void Update() {
        if (Shape.Position.Y > 1 || Shape.Position.Y < 0)
            DeleteEntity();
        Shape.Move();
    }
    public override void Render() {
        RenderEntity();
    }
}