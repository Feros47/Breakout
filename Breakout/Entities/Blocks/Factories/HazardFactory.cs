namespace Breakout.Entities.Blocks.Factories;

using Breakout.Entities.Blocks.Decorators;
using Breakout.Entities.DropItem.CreationStrategies;
using Breakout.Entities.DropItem.CreationStrategies.HazardStrategies;
using Breakout.Levels;

/// <summary>
/// Hazard block factory.
/// </summary>
public class HazardFactory : IBlockFactory {
    /// <summary>
    /// Decorate a block with a <see cref="HazardBlockDecorator"/>
    /// </summary>
    /// <param name="block">The block to decorate.</param>
    /// <param name="images">Image-pair of the block.</param>
    /// <returns>A decorated version of the input block.</returns>
    public BaseBlock Decorate(BaseBlock block, IBlockImagePair images) {
        return new HazardBlockDecorator(block, CreateStrategy());
    }

    private static Random rng = new Random();
    private IDropItemCreationStrategy CreateStrategy() {
        var i = rng.Next(1, 5);
        switch (i) {
            case 1:
                return new DecreaseHealthStrategy();
            case 2:
                return new DecreasePlayerSpeedStrategy();
            case 3:
                return new SlimJimStrategy();
            default:
                return new TimerSpeedUpStrategy();
        }
    }
}
