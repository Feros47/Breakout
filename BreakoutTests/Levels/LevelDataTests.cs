namespace BreakoutTests.Levels;

using System.Linq;
using Breakout.Entities.Blocks;
using Breakout.Entities.Blocks.Decorators;
using Breakout.Entities.Blocks.Factories;
using Breakout.Levels;
using DIKUArcade.Math;

[TestFixture]
public class LevelDataTests {
    private LevelData levelData;
    [SetUp]
    public void SetUp() {
        levelData = new LevelData() {
            AsciiMap = new AsciiMapContainer(new Vec2I(8, 8)),
            Metadata = new Metadata(),
            LegendData = new LegendData()
        };
        levelData.AsciiMap.Add(new MapElement(new Vec2I(0, 0), 'x'));
        levelData.AsciiMap.Add(new MapElement(new Vec2I(1, 0), 'y'));
        levelData.AsciiMap.Add(new MapElement(new Vec2I(2, 0), 'z'));
        levelData.AsciiMap.Add(new MapElement(new Vec2I(3, 0), 'w'));

        levelData.Metadata["Hardened"] = "x";
        levelData.Metadata.Factories.Add('x', new HardenedFactory());

        levelData.Metadata["Unbreakable"] = "y";
        levelData.Metadata.Factories.Add('y', new UnbreakableFactory());

        levelData.Metadata["PowerUp"] = "z";
        levelData.Metadata.Factories.Add('z', new PowerUpFactory());

        levelData.LegendData.AddLegend('x', "brown-block.png");
        levelData.LegendData.AddLegend('y', "brown-block.png");
        levelData.LegendData.AddLegend('z', "brown-block.png");
        levelData.LegendData.AddLegend('w', "brown-block.png");
    }

    [Test]
    public void C1TestCreateBlocks() {
        var blocks = levelData.CreateBlocks();

        Assert.Multiple(() => {
            Assert.That(blocks.Count(), Is.EqualTo(4));
            Assert.That(blocks[0].IsBlockType<HardenedBlockDecorator>(), Is.True);
            Assert.That(blocks[1].IsBlockType<UnbreakableBlockDecorator>(), Is.True);
            Assert.That(blocks[2].IsBlockType<PowerUpBlockDecorator>(), Is.True);
            Assert.That(blocks[3].IsBlockType<Block>(), Is.True);
        });

    }
}
