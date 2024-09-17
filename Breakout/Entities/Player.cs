namespace Breakout.Entities;

using System;
using Breakout.Entities.Collidable;
using Breakout.Entities.Collidable.Visitors;
using Breakout.Utility;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Physics;

/// <summary>
/// Player is a class that represents the player in the game.
/// </summary>
public class Player : BreakoutEntityBase, IGameEventProcessor, ICollidable {
    public const float MOVEMENT_SPEED_INITIAL = 0.013f;
    private readonly float initialWidth;
    private float width;
    private float moveLeft = 0.0f;
    private float moveRight = 0.0f;
    private float movementSpeed;

    /// <summary>
    /// Gives the player movement speed a public get and a private set, and limits the speed to a range.
    /// </summary>
    /// <returns>float</returns>
    public float MovementSpeed {
        get => movementSpeed;
        private set {
            if (value > 2f * MOVEMENT_SPEED_INITIAL)
                return;

            if (value < 0.5f * MOVEMENT_SPEED_INITIAL)
                return;

            movementSpeed = value;
        }
    }

    /// <summary>
    /// Gives the player width a public get and a private set, and limits the width to a range.
    /// </summary>
    /// <returns>float</returns>
    public float Width {
        get => width;
        private set {
            if (value > 2f * initialWidth)
                return;

            if (value < 0.5f * initialWidth)
                return;

            width = value;
        }
    }

    /// <summary>
    /// Constructor for the player.
    /// </summary>
    /// <param name="image">Image of the player</param>
    /// <returns>Player</returns>
    public Player(IBaseImage image) : base(new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.15f, 0.03f)), image) {
        BreakoutBus
            .GetBus()
            .Subscribe(GameEventType.PlayerEvent, this);
        MovementSpeed = MOVEMENT_SPEED_INITIAL;
        movementSpeed = MOVEMENT_SPEED_INITIAL;
        initialWidth = Shape.Extent.X;
        Width = initialWidth;
    }

    /// <summary>
    /// Reset the player to its initial state.
    /// </summary>
    public void Reset() {
        Shape = new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.15f, 0.03f));

        Shape.AsDynamicShape().Direction.X = 0;
    }

    /// <summary>
    /// Get the position of the player.
    /// </summary>
    /// <returns>Vec2F</returns>
    public Vec2F GetPosition() {
        return Shape.Position;
    }

    /// <summary>
    /// Get the direction of the player.
    /// </summary>
    /// <returns>Vec2F</returns>
    public Vec2F GetDirection() {
        return Shape.AsDynamicShape().Direction;
    }

    #region BreakoutEntity
    /// <summary>
    /// Render the player.
    /// </summary>
    public override void Render() {
        RenderEntity();
    }

    /// <summary>
    /// Update the player, and clamp the player to the screen.
    /// </summary>
    public override void Update() {
        Shape.Position.X = Math.Clamp(
            Shape.Position.X + Shape.AsDynamicShape().Direction.X,
            0.0f,
            1.0f - Shape.Extent.X);
    }

    #endregion

    #region  IGameEventProcessor
    /// <summary>
    /// Process a game event.
    /// </summary>
    /// <param name="gameEvent">Game event to process</param>
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.PlayerEvent) {
            string message = gameEvent.Message;
            int index = message.IndexOf("PLAYER_");

            string action = message.Substring(index + "PLAYER_".Length);

            switch (action) {
                case "INCREASESPEED":
                    IncreaseSpeed();
                    return;
                case "DECREASESPEED":
                    DecreaseSpeed();
                    return;
                case "INCREASEWIDTH":
                    IncreaseWidth();
                    return;
                case "DECREASEWIDTH":
                    DecreaseWidth();
                    return;
            }
        }

        if (gameEvent.EventType == GameEventType.PlayerEvent) {
            var isKeyPress = gameEvent.ActionType() == KeyboardAction.KeyPress;
            switch ((KeyboardKey) gameEvent.IntArg1) {
                case KeyboardKey.D:
                    SetMoveRight(isKeyPress);
                    break;
                case KeyboardKey.A:
                    SetMoveLeft(isKeyPress);
                    break;
            }
        }
    }
    #endregion

    /// <summary>
    /// Update the direction of the player.
    /// </summary>
    private void UpdateDirection() {
        Shape.AsDynamicShape().Direction.X = moveLeft + moveRight;
    }

    /// <summary>
    /// Set the player to move left or not.
    /// </summary>
    private void SetMoveLeft(bool val) {
        if (val) {
            moveLeft = -MovementSpeed;
        } else {
            moveLeft = 0.0f;
        }
        UpdateDirection();
    }

    /// <summary>
    /// Set the player to move right or not.
    /// </summary>
    private void SetMoveRight(bool val) {
        if (val) {
            moveRight = MovementSpeed;
        } else {
            moveRight = 0.0f;
        }
        UpdateDirection();
    }

    #region PowerUp
    /// <summary>
    /// Increase the speed of the player.
    /// </summary>
    private void IncreaseSpeed() {
        if (MovementSpeed >= MOVEMENT_SPEED_INITIAL) {
            Image = DIKUArcadeExtensions.LoadStridesFromAssets("playerStride.png", 3);
        }

        var speedIncrease = 2f;
        MovementSpeed *= speedIncrease;
        moveLeft *= speedIncrease;
        moveRight *= speedIncrease;
        UpdateDirection();
    }

    /// <summary>
    /// Decrease the speed of the player.
    /// </summary>
    private void DecreaseSpeed() {
        Image = DIKUArcadeExtensions.LoadImageFromAssets("player.png");
        var speedBuff = 0.5f;
        MovementSpeed *= speedBuff;
        moveLeft *= speedBuff;
        moveRight *= speedBuff;
        UpdateDirection();
    }

    /// <summary>
    /// Increase the width of the player.
    /// </summary>
    private void IncreaseWidth() {
        Width *= 2;
        Shape.Extent.X = Width;
    }

    /// <summary>
    /// Decrease the width of the player.
    /// </summary>
    private void DecreaseWidth() {
        Width /= 2;
        Shape.Extent.X = Width;
    }

    #endregion PowerUp

    #region ICollidable
    /// <summary>
    /// Accept a collision with a visitor.
    /// </summary>
    public void AcceptCollision(ICollisionVisitor collisionVisitor, CollisionData collisionData) {
        collisionVisitor.Collide(this, collisionData);
    }

    /// <summary>
    /// Make a visitor for the player.
    /// </summary>
    /// <returns>PlayerCollisionVisitor</returns>
    public ICollisionVisitor MakeVisitor() {
        return new PlayerCollisionVisitor(this);
    }

    /// <summary>
    /// Get the shape of the player.
    /// </summary>
    /// <returns>DynamicShape</returns>
    public DynamicShape GetShape() {
        return Shape.AsDynamicShape();
    }

    /// <summary>
    /// Check if the player should be ignored.
    /// </summary>
    /// <returns>bool</returns>
    public bool ShouldIgnore() {
        return false;
    }
    #endregion
}
