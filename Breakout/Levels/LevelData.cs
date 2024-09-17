#nullable disable
namespace Breakout.Levels;

using Breakout.Entities.Blocks;
using Breakout.Entities.Blocks.Decorators;
using Breakout.Entities.Blocks.Factories;
using DIKUArcade.Entities;
using DIKUArcade.Math;

/// <summary>
/// Represents an entire level configuration, including the ASCII-map, metadata and legend fields.
/// </summary>
public class LevelData {
    private static readonly Random rng = new();
    public const double HAZARD_PROBABILITY = 0.15;
    public AsciiMapContainer AsciiMap {
        get; set;
    }
    public Metadata Metadata {
        get; set;
    }
    public LegendData LegendData {
        get; set;
    }


    /// <summary>
    /// Creates a list of <see cref="BaseBlock"/> blocks from the loaded configuration.
    /// </summary>
    /// <returns>A <see cref="List{BaseBlock}"/> containing all blocks as specified in the level file.</returns>
    public List<BaseBlock> CreateBlocks() {
        var hazardFactory = new HazardFactory();
        var blocks = new List<BaseBlock>(AsciiMap.Count);

        foreach (var element in AsciiMap) {
            var blockType = element.BlockType;
            var gridPosition = element.GridPosition;
            var images = LegendData[blockType];

            // Create a base-block using the position and extent as specified in the ASCII-map.
            var shape = new StationaryShape(
                AsciiMap.ToDecimalCoordinates(gridPosition),
                new Vec2F(AsciiMap.ColumnWidth, AsciiMap.RowHeight));
            BaseBlock block = new Block(
                shape,
                images.HealthyBlockImage);

            // Now we decorate the block using the metadata attributes.
            foreach (var factory in Metadata.Factories[blockType]) {
                block = factory.Decorate(block, images);
            }

            if (!IsSpecialBlock(block) &&
                rng.NextDouble() <= HAZARD_PROBABILITY) {
                block = hazardFactory.Decorate(block, images);
            }

            blocks.Add(block);
        }
        return blocks;
    }

    private bool IsSpecialBlock(BaseBlock block) {
        return
            block.IsBlockType<UnbreakableBlockDecorator>() ||
            block.IsBlockType<HardenedBlockDecorator>() ||
            block.IsBlockType<PowerUpBlockDecorator>() ||
            block.IsBlockType<MovingBlockDecorator>() ||
            block.IsBlockType<HazardBlockDecorator>();
    }
}
