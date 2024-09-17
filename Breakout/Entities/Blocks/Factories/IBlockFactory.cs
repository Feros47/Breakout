namespace Breakout.Entities.Blocks.Factories;

using Breakout.Levels;

/// <summary>
/// Interface of a block factory, implementing the factory method pattern.
/// </summary>
public interface IBlockFactory {
    /// <summary>
    /// Decorate a <see cref="BaseBlock"/> with the current implementation's decorator.
    /// </summary>
    /// <param name="block">The block to decorate.</param>
    /// <param name="images">Images of the block (used by e.g. HardenedFactory)</param>
    /// <return>The input block decorated with this factory's decorator type.</returns>
    BaseBlock Decorate(BaseBlock block, IBlockImagePair images);
}
