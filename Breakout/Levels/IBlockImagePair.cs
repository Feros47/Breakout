#nullable disable
namespace Breakout.Levels;

using DIKUArcade.Graphics;

/// <summary>
/// Interface with two images, that all blocks have:
/// (1) A "healthy" image
/// (2) A damaged image where the block visually has cracks.
/// </summary>
public interface IBlockImagePair {
    public IBaseImage HealthyBlockImage {
        get;
    }
    public IBaseImage DamagedBlockImage {
        get;
    }
}
