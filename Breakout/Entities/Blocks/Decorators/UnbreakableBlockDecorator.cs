namespace Breakout.Entities.Blocks.Decorators;

using Breakout.Entities.Collidable;
using Breakout.Entities.Collidable.Visitors;
using DIKUArcade.Events;

/// <summary>
/// Decorator for the "Unbreakable"-block type. Cannot die.
/// </summary>
public class UnbreakableBlockDecorator : BaseBlockDecorator {
    public UnbreakableBlockDecorator(BaseBlock decorated) : base(decorated) {
        Value = 0;
    }

    public override void HandleCollision() {
        // Intentional no-op
    }
    public override ICollisionVisitor MakeVisitor() {
        return new UnbreakableBlockCollisionVisitor();
    }
    public override void ProcessEvent(GameEvent gameEvent) {
    }
}
