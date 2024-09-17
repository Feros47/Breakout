namespace Breakout.Entities.Blocks.Decorators;

using Breakout.Entities.DropItem.CreationStrategies;
using DIKUArcade.Entities;
using DIKUArcade.Events;

/// <summary>
/// "Hazard" block type: Throws a hazard element when it is killed, which if picked up will put the player at a disadvantage.
/// </summary>
public class HazardBlockDecorator : BaseBlockDecorator {
    private IDropItemCreationStrategy strategy;
    public HazardBlockDecorator(BaseBlock decorated, IDropItemCreationStrategy strategy) : base(decorated) {
        this.strategy = strategy;
    }

    /// <summary>
    /// Handle the collision of the block, and create an item.
    /// </summary>
    public override void HandleCollision() {
        BreakoutBus
            .GetBus()
            .RegisterEvent(new GameEvent {
                EventType = GameEventType.GraphicsEvent,
                Message = "HAZARD_ITEM",
                ObjectArg1 = strategy.CreateItem(Shape.Position)
            });
        base.HandleCollision();
    }
}
