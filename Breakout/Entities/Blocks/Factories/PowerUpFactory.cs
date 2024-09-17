namespace Breakout.Entities.Blocks.Factories;

using Breakout.Entities.Blocks.Decorators;
using Breakout.Entities.DropItem.CreationStrategies;
using Breakout.Entities.DropItem.CreationStrategies.PowerUpStrategies;
using Breakout.Levels;

/// <summary>
/// PowerUp block factory.
/// </summary>
public class PowerUpFactory : IBlockFactory {
    private static readonly Random rng = new Random();
    /// <summary>
    /// Decorate a block with a <see cref="PowerUpBlockDecorator"/>
    /// </summary>
    /// <param name="block">The block to decorate.</param>
    /// <param name="images">Image-pair of the block.</param>
    /// <returns>A decorated version of the input block.</returns>
    public BaseBlock Decorate(BaseBlock block, IBlockImagePair _/*unused*/) {
        return new PowerUpBlockDecorator(block, CreateStrategy());
    }
    private IDropItemCreationStrategy CreateStrategy() {
        var i = rng.Next(1, 8);
        switch (i) {
            case 1:
                return new LaserStrategy();
            case 2:
                return new BallSplitStrategy();
            case 3:
                return new PlayerSpeedStrategy();
            case 4:
                return new PlayerWidthStrategy();
            case 5:
                return new PlayerHealthStrategy();
            case 6:
                return new HardBallStrategy();
            default:
                return new RocketStrategy();
        }
    }
}
