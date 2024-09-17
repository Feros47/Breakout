namespace BreakoutTests.Levels.Components.LeafImpl;
using Breakout.Exceptions;
using Breakout.Levels;
using Breakout.Levels.Components.LeafImpl;

[TestFixture]
public class LegendAttributeParserTests {

    [Test]
    public void TestNoSeparator() {
        Assert.Throws<ParsingException>(() => new LegendAttributeParser("hello world"));
    }
    [TestCase("ø) world")]  // a single non-ascii character
    [TestCase("he) world")] // More than one character
    public void TestNoSingleAsciiCharKey(string testVal) {
        Assert.Throws<ParsingException>(() => new LegendAttributeParser(testVal));
    }
    [Test]
    public void TestCorrectInput() {
        var parser = new LegendAttributeParser("h) world");
        Assert.That(
            parser.Key, Is.EqualTo('h'));
        Assert.That(
            parser.Value, Is.EqualTo("world"));
    }
    [Test]
    public void TestPopulate() {
        var ld = new LevelData {
            LegendData = new LegendData()
        };
        Assert.False(ld.LegendData.ContainsKey('h'));

        var parser = new LegendAttributeParser("h) purple-block.png");
        parser.Populate(ld);

        Assert.True(ld.LegendData.ContainsKey('h'));
    }

    [Test]
    public void TestPopulateKeyAlreadyExists() {
        var ld = new LevelData {
            LegendData = new LegendData()
        };
        ld.LegendData.AddLegend('k', "brown-block.png");
        var parser = new LegendAttributeParser("k) brown-block.png");
        Assert.Throws<ParsingException>(() => parser.Populate(ld));
    }

    [Test]
    public void TestNonExistentFile() {
        var ld = new LevelData {
            LegendData = new LegendData()
        };
        var parser = new LegendAttributeParser("k) ahashfhldd");
        Assert.Throws<ParsingException>(() => parser.Populate(ld));
    }
}
