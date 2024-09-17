namespace Breakout.Entities.Blocks.Factories;

using Breakout.Entities.Blocks.Decorators;
using Breakout.Levels;

/// <summary>
/// Moving block factory.
/// </summary>
public class MovingFactory : IBlockFactory {
    private float xDir = 1.0f;
    /// <summary>
    /// Decorate a block with a <see cref="MovingBlockDecorator"/>
    /// </summary>
    /// <param name="block">The block to decorate.</param>
    /// <param name="images">Image-pair of the block.</param>
    /// <returns>A decorated version of the input block.</returns>
    public BaseBlock Decorate(BaseBlock block, IBlockImagePair _/**/) {
        xDir *= -1.0f;
        return new MovingBlockDecorator(block, xDir);
    }
}
