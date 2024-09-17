namespace Breakout.Entities.Blocks;
using Breakout.Entities.Collidable;
using Breakout.Entities.Collidable.Visitors;
using Breakout.Utility;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;

/// <summary>
/// Base block class, from which all block-functionality inherits.
/// </summary>
public abstract class BaseBlock : IBreakoutEntity, ICollidable, IGameEventProcessor {
    public abstract int Value {
        get; set;
    }
    public abstract int Health {
        get; set;
    }
    public abstract Shape Shape {
        get; set;
    }
    public abstract IBaseImage Image {
        get; set;
    }

    /// <summary>
    /// Unsubscribe the current block from the <see cref="BreakoutBus"/>
    /// </summary>
    public abstract void UnsubscribeFromEventBus();
    /// <summary>
    /// Handle a collision with an arbitrary object.
    /// </summary>
    public abstract void HandleCollision();
    /// <summary>
    /// Render the block
    /// </summary>
    public abstract void Render();
    /// <summary>
    /// Update the block's internal state by a single timestep.
    /// </summary>
    public abstract void Update();
    /// <summary>
    /// Determine whether or not the entity is deleted.
    /// </summary>
    /// <returns>True if the block should be deleted/is dead.</returns>
    public abstract bool IsDeleted();
    /// <summary>
    /// Get the <see cref="Shape"/> of the block as a <see cref="DynamicShape"/>.
    /// </summary>
    /// <returns>A <see cref="DynamicShape"/> object.</returns>
    public abstract DynamicShape GetShape();
    /// <summary>
    /// Perform operations required to delete the block from the game.
    /// </summary>
    public abstract void DeleteBlock();
    /// <summary>
    /// Process an event.
    /// </summary>
    /// <param name="gameEvent">The event to process.</param>
    public abstract void ProcessEvent(GameEvent gameEvent);

    public virtual void AcceptCollision(ICollisionVisitor collisionVisitor, CollisionData collisionData) {
        collisionVisitor.Collide(this, collisionData);
    }
    public virtual ICollisionVisitor MakeVisitor() {
        return new BlockCollisionVisitor();
    }
    /// <summary>
    /// Determine whether or not this block should be ignored by the collision handling logic.
    /// </summary>
    /// <returns>True if the block is deleted, false otherwise-</returns>
    public virtual bool ShouldIgnore() {
        if (IsDeleted()) {
            return true;
        }
        return false;
    }
    public virtual bool IsBlockType<T>() where T : BaseBlock {
        return this is T;
    }

    /// <summary>
    /// Determine whether or not a <see cref="GameEvent"/> represents an explosion happening in close proximity to the current block, killing it.
    /// </summary>
    /// <param name="gameEvent">The event in question.</param>
    /// <returns>True if the explosion is near to block, and the block should die. False otherwise.</returns>
    public bool IsRocketExplosionNearby(GameEvent gameEvent) {
        if (gameEvent.ObjectArg1 is AnimationAdapter aa) {
            var t = gameEvent.EventType == GameEventType.GraphicsEvent &&
                gameEvent.Message == "ROCKET_EXPLOSION";

            var p1 = aa.Shape.Position + aa.Shape.Extent / 2.0f;
            var p2 = Shape.Position + Shape.Extent / 2.0f;

            return t && DIKUArcadeExtensions.CalculateEuclidianDistance(p1, p2) <= 0.18f;
        }
        return false;
    }
}
