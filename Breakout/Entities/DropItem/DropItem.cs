namespace Breakout.Entities.DropItem;

using Breakout.Entities.Collidable;
using Breakout.Entities.Collidable.Visitors;
using Breakout.Entities.DropItem.CreationStrategies;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;

/// <summary>
/// Class representing the droppable powerup/hazard
/// </summary>
public class DropItem : BreakoutEntityBase, ICollidable {
    public IDropItemCreationStrategy PowerUpCreationStrategy {
        get;
        private set;
    }
    /// <summary>
    /// Constructor for DropItem
    /// </summary>
    /// <param name="shape">Shape of the item</param>
    /// <param name="image">Image of the item</param>
    /// <param name="strategy">Creation strategy for the item</param>
    /// <returns>DropItem</returns>
    public DropItem(DynamicShape shape, IBaseImage image, IDropItemCreationStrategy strategy) : base(shape.AsDynamicShape(), image) {
        shape.AsDynamicShape().ChangeDirection(new Vec2F(0.0f, -0.01f));
        PowerUpCreationStrategy = strategy;
    }
    #region ICollidable
    /// <summary>
    /// Accept a collision with a visitor.
    /// </summary>
    /// <param name="collisionVisitor">Visitor to accept collision with.</param>
    /// <param name="collisionData">Data about the collision.</param>
    public void AcceptCollision(ICollisionVisitor collisionVisitor, CollisionData collisionData) {
        collisionVisitor.Collide(this, collisionData);
    }
    /// <summary>
    /// Make a visitor for the item.
    /// </summary>
    /// <returns>DropItemCollisionVisitor</returns>
    public ICollisionVisitor MakeVisitor() {
        return new DropItemCollisionVisitor();
    }
    /// <summary>
    /// Get the shape of the item.
    /// </summary>
    /// <returns>DynamicShape</returns>
    public DynamicShape GetShape() {
        return Shape.AsDynamicShape();
    }
    /// <summary>
    /// Check if the item should be ignored.
    /// </summary>
    /// <returns>bool</returns>
    public bool ShouldIgnore() {
        if (IsDeleted())
            return true;

        return false;
    }
    #endregion
    /// <summary>
    /// Move the item
    /// </summary>
    public override void Update() {
        Shape.Move();
    }
    /// <summary>
    /// Render the item
    /// </summary>
    public override void Render() {
        RenderEntity();
    }
    /// <summary>
    /// Create the expendable of the item
    /// </summary>
    public void ExpendPowerUp() {
        PowerUpCreationStrategy.CreateExpendable(Shape.Position);
    }
}