namespace Breakout.Entities.Blocks.Factories;

using Breakout.Entities.Blocks.Decorators;
using Breakout.Levels;

/// <summary>
/// Unbreakable block factory.
/// </summary>
public class UnbreakableFactory : IBlockFactory {
    /// <summary>
    /// Decorate a block with a <see cref="UnbreakableBlockDecorator"/>
    /// </summary>
    /// <param name="block">The block to decorate.</param>
    /// <param name="images">Image-pair of the block.</param>
    /// <returns>A decorated version of the input block.</returns>
    public BaseBlock Decorate(BaseBlock block, IBlockImagePair _/*unused*/) {
        return new UnbreakableBlockDecorator(block);
    }
}
