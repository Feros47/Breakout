namespace Breakout.Entities.Blocks.Decorators;

using DIKUArcade.Graphics;

/// <summary>
/// Decorator providing the implementation of the "Hardened" block type.
/// This type will have double the health and value, requiring two hits to die.
/// </summary>
public class HardenedBlockDecorator : BaseBlockDecorator {
    private readonly IBaseImage damagedImage;
    private readonly int baseHealth;

    public HardenedBlockDecorator(BaseBlock wrapee, IBaseImage damagedImg) : base(wrapee) {
        damagedImage = damagedImg;
        baseHealth = Health;
        Health = 2 * baseHealth;
        Value = 2 * Value;
    }

    /// <summary>
    /// Handle a collision by only decrementing health if the block hasn't been hit previously.
    /// Otherwise, let the decorated block handle the collision.
    /// </summary>
    public override void HandleCollision() {
        // Block can't die during this collision
        if (Health - baseHealth > 0) {
            Health -= baseHealth;
            return;
        }
        base.HandleCollision();
    }
    /// <summary>
    /// If the block has been hit, render a damaged image, otherwise render the default image.
    /// </summary>
    public override void Render() {
        if (Health <= baseHealth) {
            Image = damagedImage;
        }
        base.Render();
    }
}
