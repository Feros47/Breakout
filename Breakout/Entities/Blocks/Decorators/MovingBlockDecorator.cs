namespace Breakout.Entities.Blocks.Decorators;

using Breakout.Entities.Collidable;
using DIKUArcade.Math;
using DIKUArcade.Physics;

/// <summary>
/// Decorator for the "Moving" block type, which if there is space, moves horizontally.
/// </summary>
public class MovingBlockDecorator : BaseBlockDecorator {
    public const float MOVEMENT_SPEED = 0.013f;
    private static readonly Random rng = new Random();
    private bool hasCollided;
    public MovingBlockDecorator(BaseBlock decorated, float xDir) : base(decorated) {
        Shape = Shape.AsDynamicShape();
        Shape.AsDynamicShape().Direction = new Vec2F(xDir * MOVEMENT_SPEED, 0.0f);
        hasCollided = false;
    }
    public Vec2F Position => Shape.Position;
    public Vec2F Extent => Shape.Extent;
    public Vec2F Direction => Shape.AsDynamicShape().Direction;

    public override void Update() {
        if (!hasCollided)       /// If the moving block is colliding with another block 
            Shape.Move();       /// then it should not move this update "cycle"
        CheckBoundary();
        base.Update();
        hasCollided = false;
    }

    /// <summary>
    /// Accept the collision and call the collisionVisitor to handle the collision
    /// </summary>
    /// <param name="collisionVisitor"></param>
    /// <param name="collisionData"></param>
    public override void AcceptCollision(ICollisionVisitor collisionVisitor, CollisionData collisionData) {
        collisionVisitor.Collide(this as MovingBlockDecorator, collisionData);
        base.AcceptCollision(collisionVisitor, collisionData);
    }

    /// <summary>
    /// Change the direction of the block if it collides with another block, and set the hasCollided flag true
    /// </summary>
    /// <param name="direction"></param>
    public void ChangeDirection(CollisionDirection direction) {
        if (direction == CollisionDirection.CollisionDirLeft ||
            direction == CollisionDirection.CollisionDirRight) {
            hasCollided = true;
            Direction.X *= -1.0f;
        }
    }

    /// <summary>
    /// Check if the block is at the boundary of the screen, and change direction if it is.
    /// </summary>
    private void CheckBoundary() {
        if (Position.X <= 0.0f || Position.X >= 1.0f - Extent.X) {
            Direction.X *= -1;
        }
        Position.X = (float) Math.Clamp(Position.X, 0.0, 1.0 - Extent.X);
    }
}
