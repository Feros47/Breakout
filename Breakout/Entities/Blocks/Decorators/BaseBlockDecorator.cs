namespace Breakout.Entities.Blocks.Decorators;

using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;

/// <summary>
/// Base class for all decorators. It provides default implementations to many <see cref="BaseBlock"/> methods, where the
/// call is just deferred to the decorated object.
/// </summary>
public abstract class BaseBlockDecorator : BaseBlock {
    private readonly BaseBlock decoratedBlock;
    public BaseBlockDecorator(BaseBlock decorated) {
        decoratedBlock = decorated;

        UnsubscribeFromEventBus();
        BreakoutBus
            .GetBus()
            .Subscribe(GameEventType.GraphicsEvent, this);
    }
    public override int Health {
        get => decoratedBlock.Health;
        set => decoratedBlock.Health = value;
    }
    public override int Value {
        get => decoratedBlock.Value;
        set => decoratedBlock.Value = value;
    }
    public override IBaseImage Image {
        get => decoratedBlock.Image;
        set => decoratedBlock.Image = value;
    }
    public override Shape Shape {
        get => decoratedBlock.Shape;
        set => decoratedBlock.Shape = value;
    }
    public override void HandleCollision() {
        decoratedBlock.HandleCollision();
    }
    public override void Render() {
        decoratedBlock.Render();
    }
    public override void Update() {
        decoratedBlock.Update();
    }
    public override bool IsDeleted() {
        return decoratedBlock.IsDeleted();
    }
    public override DynamicShape GetShape() {
        return decoratedBlock.GetShape();
    }
    public override bool IsBlockType<T>() {
        return this is T || decoratedBlock.IsBlockType<T>();
    }
    public override void DeleteBlock() {
        decoratedBlock.DeleteBlock();
        UnsubSelf();
    }
    public override void ProcessEvent(GameEvent gameEvent) {
        decoratedBlock.ProcessEvent(gameEvent);
    }
    public override void UnsubscribeFromEventBus() {
        UnsubSelf();
        decoratedBlock.UnsubscribeFromEventBus();
    }

    private void UnsubSelf() {
        BreakoutBus
            .GetBus()
            .Unsubscribe(GameEventType.GraphicsEvent, this);
    }
}
