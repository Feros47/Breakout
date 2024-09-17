namespace Breakout.Entities.Blocks;

using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;

/// <summary>
/// <see cref="Block"/> represents a block's base case: A single-image block that gets deleted when it is hit.
/// This class implements the adapter pattern, between <see cref="Entity"/> and <see cref="BaseBlock"/>.
/// </summary>
public class Block : BaseBlock {
    // We need to use composition since Block can't inherit from both Entity and BaseBlock.
    private readonly Entity block;
    private readonly int baseHealth;
    private int health;
    private int pointValue;
    public Block(Shape shape, IBaseImage image) {
        BreakoutBus.GetBus().Subscribe(GameEventType.GraphicsEvent, this);
        block = new Entity(shape, image);
        baseHealth = 10;
        health = baseHealth;
        pointValue = 10;
    }

    public override int Health {
        get => health;
        set => health = value;
    }
    public override int Value {
        get => pointValue;
        set => pointValue = value;
    }
    public override Shape Shape {
        get => block.Shape;
        set => block.Shape = value;
    }
    public override IBaseImage Image {
        get => block.Image;
        set => block.Image = value;
    }
    /// <summary>
    /// Decrement health and, if dead, delete the block and raise an event to signal that the player's points should increase.
    /// </summary>
    public override void HandleCollision() {
        health -= baseHealth;
        if (health <= 0) {
            BreakoutBus
                .GetBus()
                .RegisterEvent(new GameEvent {
                    EventType = GameEventType.StatusEvent,
                    Message = "POINTS",
                    IntArg1 = Value
                });
            DeleteBlock();
        }
    }
    public override DynamicShape GetShape() {
        return block.Shape.AsDynamicShape();
    }
    public override void Render() {
        block.RenderEntity();
    }
    public override void Update() {
        // Intentional no-op: Blocks don't usually need updating.
    }
    public override bool IsDeleted() {
        return block.IsDeleted();
    }

    public override void DeleteBlock() {
        block.DeleteEntity();
    }
    public override void ProcessEvent(GameEvent gameEvent) {
        if (IsRocketExplosionNearby(gameEvent)) {
            DeleteBlock();
        }
    }
    public override void UnsubscribeFromEventBus() {
        BreakoutBus
            .GetBus()
            .Unsubscribe(GameEventType.GraphicsEvent, this);
    }
}
