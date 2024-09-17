namespace BreakoutTests.Levels.Components.LeafImpl;

using System.Linq;
using Breakout.Levels;
using Breakout.Levels.Components.LeafImpl;
using DIKUArcade.Math;

[TestFixture]
public class MapElementParserTests {

    [Test]
    public void TestMakesElement() {
        var parser = new MapElementParser('.', 8, 9);
        Assert.That(
            parser.Element.BlockType == '.' &&
            parser.Element.GridPosition.X == 8 &&
            parser.Element.GridPosition.Y == 9);
    }

    [Test]
    public void TestPopulate() {
        var ld = new LevelData {
            AsciiMap = new AsciiMapContainer(new Vec2I(8, 9))
        };
        Assert.False(ld.AsciiMap.Any());
        var parser = new MapElementParser('.', 8, 9);
        parser.Populate(ld);

        Assert.That(ld.AsciiMap.Count(), Is.EqualTo(1));
        Assert.That(ld.AsciiMap[0].BlockType == '.' &&
            ld.AsciiMap[0].GridPosition.X == 8 &&
            ld.AsciiMap[0].GridPosition.Y == 9);
    }
}
