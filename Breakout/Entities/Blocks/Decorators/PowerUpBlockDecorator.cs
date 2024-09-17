namespace Breakout.Entities.Blocks.Decorators;

using Breakout.Entities.DropItem.CreationStrategies;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Math;

/// <summary>
/// "Powerup" blocks: When dead, drops an item that if caught by the player, puts them at an advantage.
/// </summary>
public class PowerUpBlockDecorator : BaseBlockDecorator {
    private IDropItemCreationStrategy strategy;
    public PowerUpBlockDecorator(BaseBlock decorated, IDropItemCreationStrategy strategy) : base(decorated) {
        this.strategy = strategy;
    }

    /// <summary>
    /// Handle the collision of the block, and create a powerup item.
    /// </summary>
    public override void HandleCollision() {
        ThrowPowerUpItem();
        base.HandleCollision();
    }

    /// <summary>
    /// Check if a rocket explosion is nearby, and throw a powerup item if it is.
    /// </summary>
    /// <param name="gameEvent"></param>
    public override void ProcessEvent(GameEvent gameEvent) {
        if (IsRocketExplosionNearby(gameEvent)) {
            ThrowPowerUpItem();
        }
        base.ProcessEvent(gameEvent);
    }

    /// <summary>
    /// Renders the block and the powerup icon.
    /// </summary>
    public override void Render() {
        base.Render();

        // We want the icons to be slightly smaller than the block
        const float SHRINKAGE = 0.7f;
        var iconExtent = new Vec2F(Shape.Extent.X * SHRINKAGE, Shape.Extent.Y * SHRINKAGE);
        var iconPos = new Vec2F(
            Shape.Position.X + (Shape.Extent.X - iconExtent.X) / 2.0f,
            Shape.Position.Y + (Shape.Extent.Y - iconExtent.Y) / 2.0f);
        strategy.PowerupIcon.Render(new StationaryShape(iconPos, iconExtent));
    }

    private void ThrowPowerUpItem() {
        BreakoutBus
            .GetBus()
            .RegisterEvent(new GameEvent {
                EventType = GameEventType.GraphicsEvent,
                Message = "POWERUP_ITEM",
                ObjectArg1 = strategy.CreateItem(Shape.Position)
            });
    }
}
