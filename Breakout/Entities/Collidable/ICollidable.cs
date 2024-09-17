namespace Breakout.Entities.Collidable;
using DIKUArcade.Entities;
using DIKUArcade.Physics;

/// <summary>
/// Base interfaces for objects that can collide with other objects.
/// </summary>
public interface ICollidable {
    /// <summary>
    /// Let a visitor "visit" the current object, by calling its Collide-specialization.
    /// </summary>
    /// <param name="collisionVisitor">The visitor.</param>
    /// <param name="collisionData">Data about the current collision.</param>
    void AcceptCollision(ICollisionVisitor collisionVisitor, CollisionData collisionData);
    /// <summary>
    /// Create a concrete <see cref="ICollisionVisitor"/> for the current object.
    /// </summary>
    /// <returns>A new <see cref="ICollisionVisitor"/> object.</returns>
    ICollisionVisitor MakeVisitor();
    /// <summary>
    /// Get the current objects, <see cref="Shape"/> as a <see cref="DynamicShape"/>
    /// </summary>
    /// <returns>A <see cref="DynamicShape"/> object.</returns>
    DynamicShape GetShape();
    /// <summary>
    /// Determine whether or not the object should be ignored by collision handling functionality.
    /// </summary>
    /// <returns>True if collision handling should not include the current object, false otherwise.</returns>
    bool ShouldIgnore();
}