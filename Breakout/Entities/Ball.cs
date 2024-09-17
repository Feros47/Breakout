namespace Breakout.Entities;

using Breakout.Entities.Collidable;
using Breakout.Entities.Collidable.Visitors;
using Breakout.Utility;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;

/// <summary>
/// Ball is a class that represents a ball in the game.
/// </summary>
public class Ball : BreakoutEntityBase, ICollidable {
    private static readonly float speed = 0.015f;
    private static readonly Random rng = new();

    /// <summary>
    /// Gives the ball direction a public get and a private set 
    /// </summary>
    public Vec2F Direction {
        get => Shape.AsDynamicShape().Direction;
        private set => Shape.AsDynamicShape().Direction = value;
    }
    public Vec2F Position {
        get => Shape.Position;
    }
    public Vec2F Extent {
        get => Shape.Extent;
    }
    public float Speed {
        get => speed;
    }

    /// <summary>
    /// Constructor for the ball.
    /// </summary>
    /// <param name="image">Image of the ball</param>
    /// <returns>Ball</returns>
    public Ball(IBaseImage image) : base(new DynamicShape(new Vec2F(0.525f, 0.101f), new Vec2F(0.03f, 0.03f)), image) {
        Shape.AsDynamicShape().Direction = MakeRandomUpwardsDirection();
    }

    #region IBreakoutEntity
    /// <summary>
    /// Render the ball.
    /// </summary>
    public override void Render() {
        RenderEntity();
    }
    /// <summary>
    /// Update the ball.
    /// </summary>
    public override void Update() {
        Shape.Move();
        CheckBoundary();
    }
    #endregion

    #region ICollidable impl
    /// <summary>
    /// Accepts a collision visitor and a collision data.
    /// </summary>
    /// <param name="collisionVisitor"></param>
    /// <param name="collisionData"></param>
    public void AcceptCollision(ICollisionVisitor collisionVisitor, CollisionData collisionData) {
        collisionVisitor.Collide(this, collisionData);
    }

    /// <summary>
    /// Make a visitor for the ball.
    /// </summary>
    /// <returns>BallCollisionVisitor</returns>
    public virtual ICollisionVisitor MakeVisitor() {
        return new BallCollisionVisitor();
    }

    /// <summary>
    /// Get the shape of the ball.
    /// </summary>
    /// <returns>DynamicShape</returns>
    public DynamicShape GetShape() {
        return Shape.AsDynamicShape();
    }

    /// <summary>
    /// Check if the ball should be ignored.
    /// </summary>
    /// <returns>bool</returns>
    public bool ShouldIgnore() {
        if (IsDeleted())
            return true;

        return false;
    }
    #endregion

    #region Ball private implementation
    /// <summary>
    /// Check if the ball is within the boundaries of the screen.
    /// If not, change the balls direction or delete it.
    /// </summary>
    private void CheckBoundary() {

        if (Position.X <= 0.0f || Position.X >= 1.0f - Extent.X) {
            Direction.X *= -1;
        }
        if (Position.Y >= 1.0f - Extent.Y) {
            Direction.Y *= -1;
        }
        if (Position.Y <= 0.0f) {
            DeleteEntity();
        }

        Position.X = (float) Math.Clamp(
                Position.X,
                0.0, 1.0 - Extent.X);
        Position.Y = (float) Math.Clamp(
                Position.Y,
                0.0, 1.0 - Extent.Y);

    }
    /// <summary>
    /// Change the direction of the ball based on the collision data.
    /// </summary>
    /// <param name="collisionData"></param>
    /// <param name="otherEntitySpeed"></param>
    public void ChangeDirection(CollisionData collisionData, Vec2F? otherEntitySpeed = null) {
        switch (collisionData.CollisionDir) {                   /// Change direction based on the collision direction
            case CollisionDirection.CollisionDirUp:
            case CollisionDirection.CollisionDirDown:
                Direction.Y *= -1;
                break;
            case CollisionDirection.CollisionDirLeft:
            case CollisionDirection.CollisionDirRight:
                Direction.X *= -1;
                break;
        }

        if (otherEntitySpeed != null) {               /// If the collided has a speed, change the direction based on that
            otherEntitySpeed = otherEntitySpeed.UnitVector() * 0.001f;
            Direction = Direction + otherEntitySpeed;
        } else if (Math.Abs(Direction.X) < 1e-4f) {
            Direction.X = 1e-4f;                      /// If the direction is too small, make it a small positive number
        }
        Direction = Direction.UnitVector() * speed;   /// Make sure the direction is a unit vector
    }

    /// <summary>
    /// Make a random upwards direction for the ball.
    /// </summary>
    /// <returns></returns>
    private static Vec2F MakeRandomUpwardsDirection() {
        var x = rng.NextSingle(); // [0;1]
        x = 2.0f * x - 1.0f; // [0;2] - 1.0f = [-1;1]
        return new Vec2F(x, 1.0f)
            .UnitVector() * speed;
    }
    #endregion

}